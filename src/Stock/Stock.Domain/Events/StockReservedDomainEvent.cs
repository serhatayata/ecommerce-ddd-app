using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockReservedDomainEvent : DomainEvent
{
    public int StockItemId { get; }
    public int ReservedQuantity { get; }
    public int OrderId { get; }

    public StockReservedDomainEvent(int stockItemId, int reservedQuantity, int orderId)
    {
        StockItemId = stockItemId;
        ReservedQuantity = reservedQuantity;
        OrderId = orderId;
    }
}
