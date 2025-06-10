using MediatR;
using OrderManagement.Application.Models;
using OrderManagement.Application.Services.Products;
using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Models.Orders;
using OrderManagement.Application.Services.PaymentSystems;

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

        public OrderAddCommandHandler(
        IOrderRepository orderRepository,
        IProductCatalogApiService productCatalogApiService,
        IPaymentSystemApiService paymentSystemApiService)
        {
            _orderRepository = orderRepository;
            _productCatalogApiService = productCatalogApiService;
            _paymentSystemApiService = paymentSystemApiService;
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

            order.RaiseOrderCreatedEvent();

            _ = await _orderRepository.SaveAsync(order, cancellationToken) > 0;

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
    }
}