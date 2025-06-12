using Common.Domain.SharedKernel;

namespace Common.Domain.ValueObjects;

public sealed class StockItemId : TypedIdValueBase<StockItemId, int>
{
    private StockItemId(int value) : base(value)
    {
    }
    
    public static StockItemId From(int value) => new(value);
}