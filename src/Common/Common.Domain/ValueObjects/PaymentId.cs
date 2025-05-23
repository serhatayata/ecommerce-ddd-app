namespace Common.Domain.ValueObjects;

public sealed class PaymentId : ValueObject
{
    public int Value { get; }

    private PaymentId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Payment ID must be a positive number", nameof(value));
            
        Value = value;
    }

    public static PaymentId From(int value) => new(value);

    public static implicit operator int(PaymentId paymentId) => paymentId.Value;
    public static implicit operator PaymentId(int value) => From(value);

    public override string ToString() => Value.ToString();
}