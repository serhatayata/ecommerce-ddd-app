using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockRemovedDomainEvent : DomainEvent
{
    public StockRemovedDomainEvent(
        int stockItemId,
        int quantity,
        DateTime removedDate,
        Guid? correlationId) 
        : base(correlationId)
    {
        StockItemId = stockItemId;
        Quantity = quantity;
        RemovedDate = removedDate;
    }

    public int StockItemId { get; set; }
    public int Quantity { get; set; }
    public DateTime RemovedDate { get; set; }
}
