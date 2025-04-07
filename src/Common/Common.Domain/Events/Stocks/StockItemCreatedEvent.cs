namespace Common.Domain.Events.Stocks;

public sealed record StockItemCreatedEvent : IntegrationEvent
{
    public StockItemCreatedEvent(
        Guid correlationId,
        Guid productId,
        int initialQuantity,
        DateTime createdDate) 
        : base(correlationId, DateTime.UtcNow)
    {
        ProductId = productId;
        InitialQuantity = initialQuantity;
        CreatedDate = createdDate;
    }

    public Guid ProductId { get; set; }
    public int InitialQuantity { get; set; }
    public DateTime CreatedDate { get; set; }
}
