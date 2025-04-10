namespace Common.Domain.Events.Stocks;

public sealed record StockItemCreatedEvent : IntegrationEvent
{
    public StockItemCreatedEvent(
        Guid correlationId,
        int productId,
        int initialQuantity,
        string warehouse,
        string aisle,
        string shelf,
        string bin,
        DateTime createdDate) 
        : base(correlationId, DateTime.UtcNow)
    {
        ProductId = productId;
        InitialQuantity = initialQuantity;
        Warehouse = warehouse;
        Aisle = aisle;
        Shelf = shelf;
        Bin = bin;
        CreatedDate = createdDate;
    }

    public int ProductId { get; set; }
    public int InitialQuantity { get; set; }
    public string Warehouse { get; set; }
    public string Aisle { get; set; }
    public string Shelf { get; set; }
    public string Bin { get; set; }   
    public DateTime CreatedDate { get; set; }
}
