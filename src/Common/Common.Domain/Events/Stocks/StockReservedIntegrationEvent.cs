using Common.Domain.Events;

namespace Common.Domain.Events.Stocks;

public sealed record StockReservedIntegrationEvent : IntegrationEvent
{
    public int StockItemId { get; }
    public int ReservedQuantity { get; }
    public int OrderId { get; }

    public StockReservedIntegrationEvent(int stockItemId, int reservedQuantity, int orderId)
    {
        StockItemId = stockItemId;
        ReservedQuantity = reservedQuantity;
        OrderId = orderId;
    }
}
