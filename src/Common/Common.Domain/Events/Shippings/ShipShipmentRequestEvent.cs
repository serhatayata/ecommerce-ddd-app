namespace Common.Domain.Events.Shippings;

public sealed record ShipShipmentRequestEvent : IntegrationEvent
{
    public int ShipmentId { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime ShippedDate { get; set; }

    public ShipShipmentRequestEvent(
        Guid correlationId,
        int shipmentId,
        string trackingNumber,
        DateTime shippedDate) : 
    base(correlationId, DateTime.UtcNow)
    {
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        ShippedDate = shippedDate;
    }
}