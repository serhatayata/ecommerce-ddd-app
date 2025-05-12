namespace Common.Domain.Events.Stocks;

public sealed record StockReserveFailedEvent : IntegrationEvent
{
    public StockReserveFailedEvent(
        Guid? correlationId,
        int? stockItemId,
        int orderId,
        int quantity,
        DateTime creationDate,
        string errorMessage) 
        : base(correlationId, DateTime.UtcNow)
    {
        StockItemId = stockItemId;
        OrderId = orderId;
        Quantity = quantity;
        CreationDate = creationDate;
        ErrorMessage = errorMessage;
    }

    public int? StockItemId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreationDate { get; set; }
    public string ErrorMessage { get; set; }
}