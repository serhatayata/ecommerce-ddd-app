using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StocksReserveFailedDomainEvent : DomainEvent
{
    public StocksReserveFailedDomainEvent(
        int orderId,
        DateTime reservedDate,
        string errorMessage,
        Guid? correlationId)
        : base(correlationId)
    {
        OrderId = orderId;
        ReservedDate = reservedDate;
        ErrorMessage = errorMessage;
    }

    public int OrderId { get; set; }
    public DateTime ReservedDate { get; set; }
    public string ErrorMessage { get; set; }
}