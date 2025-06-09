namespace Common.Domain.Events.Shippings;

public sealed record ShipShipmentRequestEvent : IntegrationEvent
{
    public ShipShipmentRequestEvent()
    {
    }

    public ShipShipmentRequestEvent(
        Guid correlationId,
        int orderId,
        DateTime shippedDate) :
    base(correlationId, DateTime.UtcNow)
    {
        ShippedDate = shippedDate;
    }
    
    public int OrderId { get; set; }
    public DateTime ShippedDate { get; set; }
}