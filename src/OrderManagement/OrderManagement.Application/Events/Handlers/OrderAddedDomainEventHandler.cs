using Common.Domain.Events.OrderManagements;
using Common.Domain.Models.DTOs.OrderManagements;
using MassTransit;
using MediatR;
using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Events;

namespace OrderManagement.Application.Events.Handlers;

public class OrderAddedDomainEventHandler : INotificationHandler<OrderAddedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IOrderRepository _orderRepository;

    public OrderAddedDomainEventHandler(
    IPublishEndpoint publishEndpoint,
    IOrderRepository orderRepository)
    {
        _publishEndpoint = publishEndpoint;
        _orderRepository = orderRepository;
    }

    public async Task Handle(
        OrderAddedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(notification.OrderId);
        var orderItems = order.OrderItems
                            .Select(s => new OrderItemDto
                            {
                                ProductId = s.ProductId,
                                Quantity = s.Quantity,
                                UnitPrice = s.UnitPrice
                            }).ToList();

        var orderAddedEvent = new OrderAddedEvent(
            notification.CorrelationId,
            notification.OrderId,
            notification.UserId,
            notification.OrderDate,
            notification.Status,
            notification.TotalAmount,
            orderItems);

        await _publishEndpoint.Publish(orderAddedEvent, cancellationToken);
    }
}