namespace Common.Domain.Events.Shippings;

public sealed record ShipShipmentRequestEvent : IntegrationEvent
{
    public int OrderId { get; set; }
    public DateTime ShippedDate { get; set; }

    public ShipShipmentRequestEvent(
        Guid correlationId,
        int orderId,
        DateTime shippedDate) : 
    base(correlationId, DateTime.UtcNow)
    {
        ShippedDate = shippedDate;
    }
}