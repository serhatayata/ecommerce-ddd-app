namespace Common.Domain.Events.Stocks;

public sealed record StockAddedEvent : IntegrationEvent
{
    public StockAddedEvent()
    {   
    }

    public StockAddedEvent(
        Guid? correlationId,
        int stockItemId,
        int quantity,
        DateTime addedDate)
        : base(correlationId, addedDate)
    {
        StockItemId = stockItemId;
        Quantity = quantity;
        AddedDate = addedDate;
    }

    public int StockItemId { get; set; }
    public int Quantity { get; set; }
    public DateTime AddedDate { get; set; }
}
