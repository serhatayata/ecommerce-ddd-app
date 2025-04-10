namespace Stock.Application.Commands.StockItems.StockAdd;

public sealed record StockAddResponse
{
    public int StockItemId { get; set; }
    public int Quantity { get; set; }
}