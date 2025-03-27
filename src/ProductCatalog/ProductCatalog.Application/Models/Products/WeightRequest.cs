using ProductCatalog.Domain.Models.Products;

namespace ProductCatalog.Application.Models.Products;

public sealed record WeightRequest
{
    public decimal Value { get; set; }
    public WeightUnit Unit { get; set; }
}