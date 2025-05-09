using Common.Domain.Events.OrderManagements;
using MassTransit;
using MediatR;
using OrderManagement.Domain.Events;

namespace OrderManagement.Application.Events.Handlers;

public class OrderAddedDomainEventHandler : INotificationHandler<OrderAddedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderAddedDomainEventHandler(
        IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        OrderAddedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var orderAddedEvent = new OrderAddedEvent(
            notification.CorrelationId,
            notification.OrderId,
            notification.UserId,
            notification.OrderDate,
            notification.Status,
            notification.TotalAmount);

        await _publishEndpoint.Publish(orderAddedEvent, cancellationToken);
    }
}