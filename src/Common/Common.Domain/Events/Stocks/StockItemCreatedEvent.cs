namespace Common.Domain.Events.Stocks;

public sealed record StockItemCreatedEvent : IntegrationEvent
{
    public StockItemCreatedEvent()
    {
    }

    public StockItemCreatedEvent(
        int stockItemId,
        int productId,
        int initialQuantity,
        string warehouse,
        string aisle,
        string shelf,
        string bin,
        DateTime createdDate,
        Guid correlationId)
        : base(correlationId, DateTime.UtcNow)
    {
        StockItemId = stockItemId;
        ProductId = productId;
        InitialQuantity = initialQuantity;
        Warehouse = warehouse;
        Aisle = aisle;
        Shelf = shelf;
        Bin = bin;
        CreatedDate = createdDate;
    }

    public int StockItemId { get; set; }
    public int ProductId { get; set; }
    public int InitialQuantity { get; set; }
    public string Warehouse { get; set; }
    public string Aisle { get; set; }
    public string Shelf { get; set; }
    public string Bin { get; set; }   
    public DateTime CreatedDate { get; set; }
}
