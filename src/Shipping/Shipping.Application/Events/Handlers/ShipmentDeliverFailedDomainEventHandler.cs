using Common.Domain.Events.Shippings;
using MassTransit;
using MediatR;
using Shipping.Domain.Events;

namespace Shipping.Application.Events.Handlers;

public class ShipmentDeliverFailedDomainEventHandler : INotificationHandler<ShipmentDeliverFailedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ShipmentDeliverFailedDomainEventHandler(
    IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public Task Handle(
    ShipmentDeliverFailedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var shipmentDeliverFailedEvent = new ShipmentDeliverFailedEvent(
            notification.CorrelationId,
            notification.ShipmentId,
            notification.CreationDate,
            notification.ErrorMessage);

        return _publishEndpoint.Publish(shipmentDeliverFailedEvent, cancellationToken);
    }
}