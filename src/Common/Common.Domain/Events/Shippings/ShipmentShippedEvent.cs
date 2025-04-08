namespace Common.Domain.Events.Shippings;

public sealed record ShipmentShippedEvent : IntegrationEvent
{
    public ShipmentShippedEvent(
        Guid correlationId,
        int shipmentId,
        string trackingNumber,
        DateTime shippedDate)
        : base(correlationId, DateTime.UtcNow)
    {
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        ShippedDate = shippedDate;
    }

    public int ShipmentId { get; }
    public string TrackingNumber { get; }
    public DateTime ShippedDate { get; }
}