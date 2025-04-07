namespace Common.Domain.Events.Stocks;

public sealed record StockRemovedEvent : IntegrationEvent
{
    public StockRemovedEvent(
        Guid correlationId,
        Guid productId,
        int quantity,
        DateTime removedDate) 
        : base(correlationId, DateTime.UtcNow)
    {
        ProductId = productId;
        Quantity = quantity;
        RemovedDate = removedDate;
    }

    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime RemovedDate { get; set; }
}
