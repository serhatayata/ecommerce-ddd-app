namespace Common.Domain.Events.Stocks;

public sealed record StockItemCreateRequestEvent : IntegrationEvent
{
    public int StockItemId { get; init; }
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public string Warehouse { get; set; }
    public string Aisle { get; set; }
    public string Shelf { get; set; }
    public string Bin { get; set; }    
    public DateTime CreatedDate { get; init; }

    public StockItemCreateRequestEvent(
        int stockItemId,
        int productId,
        int quantity,
        string warehouse,
        string aisle,
        string shelf,
        string bin)
    {
        StockItemId = stockItemId;
        ProductId = productId;
        Quantity = quantity;
        Warehouse = warehouse;
        Aisle = aisle;
        Shelf = shelf;
        Bin = bin;
        CreatedDate = DateTime.UtcNow;
    }
}