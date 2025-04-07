using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockRemovedDomainEvent : DomainEvent
{
    public int StockItemId { get; }
    public int RemovedQuantity { get; }

    public StockRemovedDomainEvent(int stockItemId, int removedQuantity)
    {
        StockItemId = stockItemId;
        RemovedQuantity = removedQuantity;
    }
}
