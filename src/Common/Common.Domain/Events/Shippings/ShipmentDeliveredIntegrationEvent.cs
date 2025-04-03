namespace Common.Domain.Events.Shippings;

public sealed record ShipmentDeliveredIntegrationEvent : IntegrationEvent
{
    public ShipmentDeliveredIntegrationEvent(
        Guid correlationId,
        Guid shipmentId,
        string trackingNumber,
        DateTime deliveredDate)
        : base(correlationId, DateTime.UtcNow)
    {
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        DeliveredDate = deliveredDate;
    }

    public Guid ShipmentId { get; }
    public string TrackingNumber { get; }
    public DateTime DeliveredDate { get; }
}