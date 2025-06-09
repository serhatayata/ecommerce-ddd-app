namespace Common.Domain.Events.Stocks;

public sealed record StockRemoveRequestEvent : IntegrationEvent
{
    public StockRemoveRequestEvent()
    {
    }

    public StockRemoveRequestEvent(
    int stockItemId,
    int removedQuantity)
    {
        StockItemId = stockItemId;
        RemovedQuantity = removedQuantity;
    }

    public int StockItemId { get; }
    public int RemovedQuantity { get; }
}
