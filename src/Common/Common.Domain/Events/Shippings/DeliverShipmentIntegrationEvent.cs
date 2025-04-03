namespace Common.Domain.Events.Shippings;

public sealed record DeliverShipmentIntegrationEvent : IntegrationEvent
{
    public DeliverShipmentIntegrationEvent(
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

    public int ShipmentId { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime DeliveredDate { get; set; }
}