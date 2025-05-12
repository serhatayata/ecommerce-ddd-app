using Common.Domain.Events.Shippings;
using MassTransit;
using MediatR;
using Shipping.Domain.Events;

namespace Shipping.Application.Events.Handlers;

public class ShipmentShipFailedDomainEventHandler : INotificationHandler<ShipmentDeliverFailedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ShipmentShipFailedDomainEventHandler(
    IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public Task Handle(
    ShipmentDeliverFailedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var shipmentShipFailedEvent = new ShipmentShipFailedEvent(
            notification.CorrelationId,
            notification.ShipmentId,
            notification.TrackingNumber,
            notification.CreationDate,
            notification.ErrorMessage);

        return _publishEndpoint.Publish(shipmentShipFailedEvent, cancellationToken);
    }
}