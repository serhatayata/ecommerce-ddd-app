using MediatR;
using OrderManagement.Application.Models;
using OrderManagement.Application.Services.Products;
using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Models.Orders;
using OrderManagement.Domain.Events;

namespace OrderManagement.Application.Commands;

public class OrderAddCommand : IRequest<OrderAddResponse>
{
    public int UserId { get; set; }

    public HashSet<OrderItemAddModel> OrderItems { get; set; }

    public class OrderAddCommandHandler : IRequestHandler<OrderAddCommand, OrderAddResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCatalogApiService _productCatalogApiService;
        private readonly IMediator _mediator;

        public OrderAddCommandHandler(
        IOrderRepository orderRepository,
        IProductCatalogApiService productCatalogApiService,
        IMediator mediator)
        {
            _orderRepository = orderRepository;
            _productCatalogApiService = productCatalogApiService;
            _mediator = mediator;
        }

        public async Task<OrderAddResponse> Handle(
            OrderAddCommand request, 
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var orderItems = request.OrderItems.Select(o => new OrderItem(
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

            var order = new Order(userId, DateTime.UtcNow);
            
            foreach (var orderItem in orderItems)
                order.AddOrderItem(orderItem);

            var isSaved = await _orderRepository.SaveAsync(order, cancellationToken) > 0;
            if (!isSaved)
            {
                var totalAmount = order.OrderItems?.Sum(x => x.UnitPrice?.Amount ?? 0) ?? 0;
                var domainEvent = new OrderAddFailedDomainEvent(
                    order.Id,
                    userId,
                    order.OrderDate,
                    totalAmount,
                    "Order could not be saved."
                );
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            // PaymentInfo creation



            return new OrderAddResponse
            {
                Id = order.Id,
                OrderDate = order.OrderDate
            };
        }
    }
}