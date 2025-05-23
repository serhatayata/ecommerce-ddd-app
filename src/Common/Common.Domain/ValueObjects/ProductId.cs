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

    public static implicit operator int(ProductId productId) => productId.Value;
    public static implicit operator ProductId(int value) => From(value);

    public override string ToString() => Value.ToString();
}