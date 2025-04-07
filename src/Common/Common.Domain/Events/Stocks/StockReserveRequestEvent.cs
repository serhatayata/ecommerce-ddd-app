namespace Common.Domain.Events.Stocks;

public sealed record StockReserveRequestEvent : IntegrationEvent
{
    public int StockItemId { get; }
    public int ReservedQuantity { get; }
    public int OrderId { get; }

    public StockReserveRequestEvent(int stockItemId, int reservedQuantity, int orderId)
    {
        StockItemId = stockItemId;
        ReservedQuantity = reservedQuantity;
        OrderId = orderId;
    }
}
