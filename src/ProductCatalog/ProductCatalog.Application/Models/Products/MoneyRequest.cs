namespace ProductCatalog.Application.Models.Products;

public sealed record MoneyRequest
{
    public decimal Amount { get; set; }
}