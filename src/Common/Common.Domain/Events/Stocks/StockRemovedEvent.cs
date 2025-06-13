namespace Common.Domain.Events.Stocks;

public sealed record StockRemovedEvent : IntegrationEvent
{
    public StockRemovedEvent()
    {
    }

    public StockRemovedEvent(
        Guid correlationId,
        int stockItemId,
        int quantity,
        DateTime removedDate)
        : base(correlationId, DateTime.UtcNow)
    {
        StockItemId = stockItemId;
        Quantity = quantity;
        RemovedDate = removedDate;
    }

    public int StockItemId { get; set; }
    public int Quantity { get; set; }
    public DateTime RemovedDate { get; set; }
}
