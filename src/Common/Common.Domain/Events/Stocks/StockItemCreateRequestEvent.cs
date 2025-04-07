namespace Common.Domain.Events.Stocks;

public sealed record StockItemCreateRequestEvent : IntegrationEvent
{
    public int StockItemId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public string Location { get; init; }
    public DateTime CreatedDate { get; init; }

    public StockItemCreateRequestEvent(
        int stockItemId,
        int productId,
        int quantity,
        string location)
    {
        StockItemId = stockItemId;
        ProductId = productId;
        Quantity = quantity;
        Location = location;
        CreatedDate = DateTime.UtcNow;
    }
}