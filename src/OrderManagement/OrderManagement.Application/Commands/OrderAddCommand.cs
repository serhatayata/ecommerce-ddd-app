using MediatR;
using OrderManagement.Application.Models;
using OrderManagement.Application.Services.Products;
using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Models.Orders;
using OrderManagement.Application.Services.PaymentSystems;
using Common.Domain.ValueObjects;

namespace OrderManagement.Application.Commands;

public class OrderAddCommand : IRequest<OrderAddResponse>
{
    public int UserId { get; set; }
    public string CardNumber { get; set; }
    public string IBAN { get; set; }
    public string CVV { get; set; }
    public string ExpirationDate { get; set; }
    public string HolderName { get; set; }

    public HashSet<OrderItemAddModel> OrderItems { get; set; }

    public class OrderAddCommandHandler : IRequestHandler<OrderAddCommand, OrderAddResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCatalogApiService _productCatalogApiService;
        private readonly IPaymentSystemApiService _paymentSystemApiService;
        private readonly IMediator _mediator;

        public OrderAddCommandHandler(
        IOrderRepository orderRepository,
        IProductCatalogApiService productCatalogApiService,
        IPaymentSystemApiService paymentSystemApiService,
        IMediator mediator)
        {
            _orderRepository = orderRepository;
            _productCatalogApiService = productCatalogApiService;
            _paymentSystemApiService = paymentSystemApiService;
            _mediator = mediator;
        }

        public async Task<OrderAddResponse> Handle(
            OrderAddCommand request,
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var orderItems = request.OrderItems.Select(o => OrderItem.Create(
                default,
                o.ProductId,
                o.Quantity,
                null
            )).ToList();

            var productIds = request.OrderItems.Select(p => p.ProductId).ToArray();

            var productsPrice = await _productCatalogApiService.GetProductsPriceByIds(productIds, cancellationToken);

            if (productsPrice != null)
                orderItems.ForEach(item =>
                {
                    var productPrice = productsPrice.List.FirstOrDefault(p => p.Id == item.ProductId);
                    if (productPrice != null)
                        item.UpdateUnitPrice(productPrice.Price);
                });

            var order = Order.Create(Common.Domain.ValueObjects.UserId.From(userId), DateTime.UtcNow);

            foreach (var orderItem in orderItems)
                order.AddOrderItem(orderItem);

            _ = await _orderRepository.SaveAsync(order, cancellationToken) > 0;

            await PublishOrderEvents(order, cancellationToken);

            var paymentInfoResponse = await _paymentSystemApiService.CreatePaymentInfoAsync(
                order.Id,
                request.CardNumber,
                request.IBAN,
                request.CVV,
                request.HolderName,
                request.ExpirationDate,
                cancellationToken
            );

            return new OrderAddResponse
            {
                Id = order.Id,
                PaymentInfoId = paymentInfoResponse.Id,
                OrderDate = order.OrderDate
            };
        }

        private async Task PublishOrderEvents(
        Order order,
        CancellationToken cancellationToken)
        {
            order.RaiseOrderCreatedEvent();

            foreach (var domainEvent in order.Events)
                await _mediator.Publish(domainEvent, cancellationToken);

            order.ClearEvents(); 
        }
    }
}