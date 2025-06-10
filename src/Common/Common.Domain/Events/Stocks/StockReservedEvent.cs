namespace Common.Domain.Events.Stocks;

public sealed record StockReservedEvent : IntegrationEvent
{
    public StockReservedEvent()
    {   
    }

    public StockReservedEvent(
        Guid correlationId,
        Guid orderId,
        DateTime reservedDate)
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        ReservedDate = reservedDate;
    }

    public Guid OrderId { get; set; }
    public DateTime ReservedDate { get; set; }
}
