using Common.Domain.Events;

namespace Common.Domain.Events.Stocks;

public sealed record StockRemovedIntegrationEvent : IntegrationEvent
{
    public int StockItemId { get; }
    public int RemovedQuantity { get; }

    public StockRemovedIntegrationEvent(int stockItemId, int removedQuantity)
    {
        StockItemId = stockItemId;
        RemovedQuantity = removedQuantity;
    }
}
