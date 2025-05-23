namespace Common.Domain.ValueObjects;

public sealed class OrderId : ValueObject
{
    public int Value { get; }

    private OrderId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Order ID must be a positive number", nameof(value));
            
        Value = value;
    }

    public static OrderId From(int value) => new(value);

    public override string ToString() => Value.ToString();
}