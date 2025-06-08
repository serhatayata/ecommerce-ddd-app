namespace Common.Domain.Events.Shippings;

public sealed record ShipmentDeliveredEvent : IntegrationEvent
{
    public ShipmentDeliveredEvent(
        Guid correlationId,
        int orderId,
        int shipmentId,
        string trackingNumber,
        DateTime deliveredDate)
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        ShipmentId = shipmentId;
        TrackingNumber = trackingNumber;
        DeliveredDate = deliveredDate;
    }

    public int ShipmentId { get; }
    public int OrderId { get; }
    public string TrackingNumber { get; }
    public DateTime DeliveredDate { get; }
}