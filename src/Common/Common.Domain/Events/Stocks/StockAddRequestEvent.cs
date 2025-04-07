namespace Common.Domain.Events.Stocks;

public sealed record StockAddRequestEvent : IntegrationEvent
{
    public int StockItemId { get; }
    public int AddedQuantity { get; }

    public StockAddRequestEvent(int stockItemId, int addedQuantity)
    {
        StockItemId = stockItemId;
        AddedQuantity = addedQuantity;
    }
}
