using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace Shipping.Domain.Events;

public sealed record ShipmentDeliveredDomainEvent : DomainEvent
{
    [JsonConstructor]
    public ShipmentDeliveredDomainEvent()
    {
    }

    public ShipmentDeliveredDomainEvent(
        Guid orderId,
        int shipmentId,
        string trackingNumber,
        DateTime deliveredDate,
        Guid? correlationId)
        : base(correlationId)
    {
        OrderId = orderId;
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        DeliveredDate = deliveredDate;
    }

    public Guid OrderId { get; }
    public int ShipmentId { get; }
    public string TrackingNumber { get; }
    public DateTime DeliveredDate { get; }
}