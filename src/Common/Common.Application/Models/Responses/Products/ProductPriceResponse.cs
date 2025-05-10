namespace Common.Application.Models.Responses.Products;

public sealed record ProductPriceResponse
{
    public int Id { get; set; }
    public decimal Price { get; set; }
}