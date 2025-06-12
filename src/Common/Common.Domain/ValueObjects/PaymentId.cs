using Common.Domain.SharedKernel;

namespace Common.Domain.ValueObjects;

public sealed class PaymentId : TypedIdValueBase<PaymentId, int>
{
    private PaymentId(int value) : base(value)
    {
    }
    
    public static PaymentId From(int value) => new(value);
}