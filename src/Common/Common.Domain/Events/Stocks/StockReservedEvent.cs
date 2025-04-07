namespace Common.Domain.Events.Stocks;

public sealed record StockReservedEvent : IntegrationEvent
{
    public StockReservedEvent(
        Guid correlationId,
        Guid productId,
        Guid orderId,
        int quantity,
        DateTime reservedDate) 
        : base(correlationId, DateTime.UtcNow)
    {
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        ReservedDate = reservedDate;
    }

    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReservedDate { get; set; }
}
