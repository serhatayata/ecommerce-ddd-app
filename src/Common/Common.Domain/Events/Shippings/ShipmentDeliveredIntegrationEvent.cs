namespace Common.Domain.Events.Shippings;

public sealed record ShipmentDeliveredIntegrationEvent : IntegrationEvent
{
    public ShipmentDeliveredIntegrationEvent(
        Guid correlationId,
        int shipmentId,
        string trackingNumber,
        DateTime deliveredDate)
        : base(correlationId, DateTime.UtcNow)
    {
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        DeliveredDate = deliveredDate;
    }

    public int ShipmentId { get; }
    public string TrackingNumber { get; }
    public DateTime DeliveredDate { get; }
}