namespace Common.Domain.Events.Shippings;

public sealed record ShipmentShippedEvent : IntegrationEvent
{
    public ShipmentShippedEvent()
    {
    }

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

    public int ShipmentId { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime ShippedDate { get; set; }
}