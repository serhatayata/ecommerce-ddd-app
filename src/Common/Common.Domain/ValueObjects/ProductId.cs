namespace Common.Domain.ValueObjects;

public sealed class ProductId : ValueObject
{
    public int Value { get; }

    private ProductId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Product ID must be a positive number", nameof(value));
            
        Value = value;
    }

    public static ProductId From(int value) => new(value);

    public override string ToString() => Value.ToString();
}