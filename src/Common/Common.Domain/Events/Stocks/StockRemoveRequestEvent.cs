namespace Common.Domain.Events.Stocks;

public sealed record StockRemoveRequestEvent : IntegrationEvent
{
    public int StockItemId { get; }
    public int RemovedQuantity { get; }

    public StockRemoveRequestEvent(int stockItemId, int removedQuantity)
    {
        StockItemId = stockItemId;
        RemovedQuantity = removedQuantity;
    }
}
