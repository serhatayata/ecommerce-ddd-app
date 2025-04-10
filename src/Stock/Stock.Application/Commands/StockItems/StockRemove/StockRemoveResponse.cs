namespace Stock.Application.Commands.StockItems.StockRemove;

public sealed record StockRemoveResponse
{
    public int StockItemId { get; set; }
    public int Quantity { get; set; }
}