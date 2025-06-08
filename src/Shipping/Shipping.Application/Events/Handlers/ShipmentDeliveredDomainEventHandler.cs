using Common.Domain.Events.Shippings;
using MassTransit;
using MediatR;
using Shipping.Domain.Events;

namespace Shipping.Application.Events.Handlers;

public class ShipmentDeliveredDomainEventHandler : INotificationHandler<ShipmentDeliveredDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ShipmentDeliveredDomainEventHandler(
    IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
    ShipmentDeliveredDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var shipmentDeliveredEvent = new ShipmentDeliveredEvent(
            notification.CorrelationId,
            notification.OrderId,
            notification.ShipmentId,
            notification.TrackingNumber,
            notification.DeliveredDate);

        await _publishEndpoint.Publish(shipmentDeliveredEvent, cancellationToken);
    }
}