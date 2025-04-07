using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockItemCreatedDomainEvent : DomainEvent
{
    public int StockItemId { get; }
    public int ProductId { get; }
    public int InitialQuantity { get; }

    public StockItemCreatedDomainEvent(int stockItemId, int productId, int initialQuantity)
    {
        StockItemId = stockItemId;
        ProductId = productId;
        InitialQuantity = initialQuantity;
    }
}
