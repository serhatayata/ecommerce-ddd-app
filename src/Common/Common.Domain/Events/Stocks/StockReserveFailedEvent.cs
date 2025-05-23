using Common.Domain.ValueObjects;

namespace Common.Domain.Events.Stocks;

public sealed record StockReserveFailedEvent : IntegrationEvent
{
    public StockReserveFailedEvent(
        Guid? correlationId,
        int orderId,
        DateTime creationDate,
        string errorMessage) 
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        CreationDate = creationDate;
        ErrorMessage = errorMessage;
    }

    public int OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    public string ErrorMessage { get; set; }
}