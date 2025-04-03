namespace Common.Domain.Events.Shippings;

public sealed record ShipShipmentIntegrationEvent : IntegrationEvent
{
    public int ShipmentId { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime ShippedDate { get; set; }

    public ShipShipmentIntegrationEvent(
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