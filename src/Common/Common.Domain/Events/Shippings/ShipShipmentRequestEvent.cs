namespace Common.Domain.Events.Shippings;

public sealed record ShipShipmentRequestEvent : IntegrationEvent
{
    public ShipShipmentRequestEvent()
    {
    }

    public ShipShipmentRequestEvent(
        Guid correlationId,
        Guid orderId,
        DateTime shippedDate) :
    base(correlationId, DateTime.UtcNow)
    {
        ShippedDate = shippedDate;
        OrderId = orderId;
    }
    
    public Guid OrderId { get; set; }
    public DateTime ShippedDate { get; set; }
}