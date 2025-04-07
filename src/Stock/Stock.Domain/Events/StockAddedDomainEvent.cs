using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockAddedDomainEvent : DomainEvent
{
    public int StockItemId { get; }
    public int AddedQuantity { get; }

    public StockAddedDomainEvent(int stockItemId, int addedQuantity)
    {
        StockItemId = stockItemId;
        AddedQuantity = addedQuantity;
    }
}
