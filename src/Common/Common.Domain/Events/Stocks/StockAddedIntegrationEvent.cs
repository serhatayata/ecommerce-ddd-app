using Common.Domain.Events;

namespace Common.Domain.Events.Stocks;

public sealed record StockAddedIntegrationEvent : IntegrationEvent
{
    public int StockItemId { get; }
    public int AddedQuantity { get; }

    public StockAddedIntegrationEvent(int stockItemId, int addedQuantity)
    {
        StockItemId = stockItemId;
        AddedQuantity = addedQuantity;
    }
}
