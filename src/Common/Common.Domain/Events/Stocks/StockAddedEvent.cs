namespace Common.Domain.Events.Stocks;

public sealed record StockAddedEvent : IntegrationEvent
{
    public StockAddedEvent(
        Guid correlationId,
        Guid productId,
        int quantity,
        DateTime addedDate) 
        : base(correlationId, DateTime.UtcNow)
    {
        ProductId = productId;
        Quantity = quantity;
        AddedDate = addedDate;
    }

    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedDate { get; set; }
}
