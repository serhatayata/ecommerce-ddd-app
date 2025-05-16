using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StocksReservedDomainEvent : DomainEvent
{
    public StocksReservedDomainEvent(
        int orderId,
        DateTime reservedDate,
        Guid? correlationId) 
        : base(correlationId)
    {
        OrderId = orderId;
        ReservedDate = reservedDate;
    }

    public int OrderId { get; set; }
    public DateTime ReservedDate { get; set; }
}
