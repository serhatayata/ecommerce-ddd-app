using Common.Domain.SharedKernel;

namespace Common.Domain.ValueObjects;

public sealed class ShipmentId : TypedIdValueBase<ShipmentId, int>
{
    private ShipmentId(int value) : base(value)
    {
    }
    
    public static ShipmentId From(int value) => new(value);
}