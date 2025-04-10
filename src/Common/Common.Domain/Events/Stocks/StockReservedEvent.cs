namespace Common.Domain.Events.Stocks;

public sealed record StockReservedEvent : IntegrationEvent
{
    public StockReservedEvent(
        Guid correlationId,
        int stockItemId,
        int orderId,
        int quantity,
        DateTime reservedDate) 
        : base(correlationId, DateTime.UtcNow)
    {
        StockItemId = stockItemId;
        OrderId = orderId;
        Quantity = quantity;
        ReservedDate = reservedDate;
    }

    public int StockItemId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReservedDate { get; set; }
}
