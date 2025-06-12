using Common.Domain.SharedKernel;

namespace Common.Domain.ValueObjects;

public sealed class ProductId : TypedIdValueBase<ProductId, int>
{
    private ProductId(int value) : base(value)
    {
    }
    
    public static ProductId From(int value) => new(value);
}