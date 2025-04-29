using Common.Domain.Events.Shippings;
using MassTransit;
using MediatR;
using Shipping.Domain.Events;

namespace Shipping.Application.Events.Handlers;

public class ShipmentShippedDomainEventHandler : INotificationHandler<ShipmentShippedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ShipmentShippedDomainEventHandler(
    IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
    ShipmentShippedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var shipmentShippedEvent = new ShipmentShippedEvent(
            notification.CorrelationId,
            notification.ShipmentId,
            notification.TrackingNumber,
            notification.ShippedDate);

        await _publishEndpoint.Publish(shipmentShippedEvent, cancellationToken);
    }
}