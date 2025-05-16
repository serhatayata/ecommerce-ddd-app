namespace Common.Domain.Events.Stocks;

public sealed record StocksReservedEvent : IntegrationEvent
{
    public StocksReservedEvent(
        Guid correlationId,
        int orderId,
        DateTime reservedDate) 
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        ReservedDate = reservedDate;
    }

    public int OrderId { get; set; }
    public DateTime ReservedDate { get; set; }
}
