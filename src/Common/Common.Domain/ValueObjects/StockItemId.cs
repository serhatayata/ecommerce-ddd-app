namespace Common.Domain.ValueObjects;

public sealed class StockItemId : ValueObject
{
    public int Value { get; }

    private StockItemId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Stock Item ID must be a positive number", nameof(value));
            
        Value = value;
    }

    public static StockItemId From(int value) => new(value);

    public override string ToString() => Value.ToString();
}