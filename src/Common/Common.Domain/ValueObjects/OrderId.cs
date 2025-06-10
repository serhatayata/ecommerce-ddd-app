using Common.Domain.SharedKernel;

namespace Common.Domain.ValueObjects;

public sealed class OrderId : TypedIdValueBase<OrderId, Guid>
{
    private OrderId(Guid value) : base(value)
    {
    }
    
    public static OrderId From(Guid value) => new(value);
}