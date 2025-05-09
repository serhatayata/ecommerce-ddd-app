using Common.Domain.Events.OrderManagements;
using MassTransit;
using MediatR;
using OrderManagement.Domain.Events;

namespace OrderManagement.Application.Events.Handlers;

public class OrderAddFailedDomainEventHandler : INotificationHandler<OrderAddFailedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderAddFailedDomainEventHandler(
        IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        OrderAddFailedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var orderAddFailedEvent = new OrderAddFailedEvent(
            notification.CorrelationId,
            notification.OrderId,
            notification.UserId,
            notification.OrderDate,
            notification.ErrorMessage);

        await _publishEndpoint.Publish(orderAddFailedEvent, cancellationToken);
    }
}