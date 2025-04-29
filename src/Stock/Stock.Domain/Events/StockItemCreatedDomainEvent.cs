using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockItemCreatedDomainEvent : DomainEvent
{
    public StockItemCreatedDomainEvent(
        int stockItemId,
        int productId,
        int initialQuantity,
        string warehouse,
        string aisle,
        string shelf,
        string bin,
        DateTime createdDate,
        Guid? correlationId) 
        : base(correlationId)
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

    public int StockItemId { get; }
    public int ProductId { get; set; }
    public int InitialQuantity { get; set; }
    public string Warehouse { get; set; }
    public string Aisle { get; set; }
    public string Shelf { get; set; }
    public string Bin { get; set; }   
    public DateTime CreatedDate { get; set; }
}
