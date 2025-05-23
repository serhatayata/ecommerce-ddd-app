namespace Common.Domain.ValueObjects;

public sealed class ShipmentId : ValueObject
{
    public int Value { get; }

    private ShipmentId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Shipment ID must be a positive number", nameof(value));
            
        Value = value;
    }

    public static ShipmentId From(int value) => new(value);

    public override string ToString() => Value.ToString();
}