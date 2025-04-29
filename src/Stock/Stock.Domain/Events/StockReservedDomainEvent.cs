using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockReservedDomainEvent : DomainEvent
{
    public StockReservedDomainEvent(
        int stockItemId,
        int orderId,
        int quantity,
        DateTime reservedDate,
        Guid? correlationId) 
        : base(correlationId)
    {
        StockItemId = stockItemId;
        OrderId = orderId;
        Quantity = quantity;
        ReservedDate = reservedDate;
    }

    public int StockItemId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReservedDate { get; set; }
}
