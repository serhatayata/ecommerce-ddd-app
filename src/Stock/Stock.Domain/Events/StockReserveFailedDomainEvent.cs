using Common.Domain.Events;

namespace Stock.Domain.Events;

public sealed record StockReserveFailedDomainEvent : DomainEvent
{
    public StockReserveFailedDomainEvent(
        int stockItemId,
        int orderId,
        int quantity,
        DateTime reservedDate,
        string errorMessage,
        Guid? correlationId)
        : base(correlationId)
    {
        StockItemId = stockItemId;
        OrderId = orderId;
        Quantity = quantity;
        ReservedDate = reservedDate;
        ErrorMessage = errorMessage;
    }

    public int StockItemId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReservedDate { get; set; }
    public string ErrorMessage { get; set; }
}