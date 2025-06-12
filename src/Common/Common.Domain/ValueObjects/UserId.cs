using Common.Domain.SharedKernel;

namespace Common.Domain.ValueObjects;

public sealed class UserId : TypedIdValueBase<UserId, int>
{
    private UserId(int value) : base(value)
    {
    }
    
    public static UserId From(int value) => new(value);
}