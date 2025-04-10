namespace Stock.Application.Commands.StockItems.StockItemCreate;

public sealed record StockItemCreateResponse
{
    public StockItemCreateResponse(
        int id,
        int productId,
        int quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }

    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}