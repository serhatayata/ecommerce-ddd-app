using Common.Domain.ValueObjects;

namespace ProductCatalog.Domain.Models.Products;

public class Weight : ValueObject
{
    public decimal Value { get; }
    public WeightUnit Unit { get; }

    private Weight(decimal value, WeightUnit unit)
    {
        if (value < 0)
            throw new ArgumentException("Weight cannot be negative", nameof(value));
            
        Value = value;
        Unit = unit;
    }

    public static Weight From(decimal value, WeightUnit unit)
    {
        return new Weight(value, unit);
    }

    public Weight ConvertTo(WeightUnit targetUnit)
    {
        if (Unit == targetUnit)
            return this;

        decimal convertedValue = Unit switch
        {
            WeightUnit.Gram when targetUnit == WeightUnit.Kilogram => Value / 1000,
            WeightUnit.Kilogram when targetUnit == WeightUnit.Gram => Value * 1000,
            _ => throw new NotSupportedException($"Conversion from {Unit} to {targetUnit} is not supported")
        };

        return new Weight(convertedValue, targetUnit);
    }

    public override string ToString() => $"{Value} {Unit}";

    public static implicit operator Weight((decimal value, WeightUnit unit) weight) 
        => From(weight.value, weight.unit);
}

public enum WeightUnit
{
    Gram,
    Kilogram
}