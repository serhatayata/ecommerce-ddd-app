using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockReservedDomainEvent : DomainEvent
{
    public StockReservedDomainEvent(
        Guid orderId,
        DateTime reservedDate,
        Guid? correlationId) 
        : base(correlationId)
    {
        OrderId = orderId;
        ReservedDate = reservedDate;
    }

    public Guid OrderId { get; set; }
    public DateTime ReservedDate { get; set; }
}
