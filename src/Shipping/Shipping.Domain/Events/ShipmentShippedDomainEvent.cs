using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace Shipping.Domain.Events;

public sealed record ShipmentShippedDomainEvent : DomainEvent
{
    [JsonConstructor]
    public ShipmentShippedDomainEvent()
    {
    }

    public ShipmentShippedDomainEvent(
        int shipmentId,
        string trackingNumber,
        DateTime shippedDate,
        Guid? correlationId)
        : base(correlationId)
    {
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        ShippedDate = shippedDate;
    }

    public int ShipmentId { get; }
    public string TrackingNumber { get; }
    public DateTime ShippedDate { get; }
}