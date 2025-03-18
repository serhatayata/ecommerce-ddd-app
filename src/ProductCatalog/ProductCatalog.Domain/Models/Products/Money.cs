using Common.Domain.Models;

namespace ProductCatalog.Domain.Models.Products;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }

    private Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("Price cannot be negative", nameof(amount));
            
        Amount = amount;
    }

    public static Money From(decimal amount)
    {
        return new Money(amount);
    }

    public Money Add(Money other)
    {
        return new Money(Amount + other.Amount);
    }

    public Money Subtract(Money other)
    {
        return new Money(Amount - other.Amount);
    }

    public Money Multiply(decimal multiplier)
    {
        return new Money(Amount * multiplier);
    }

    public override string ToString() => $"{Amount:F2}";

    public static implicit operator decimal(Money money) => money.Amount;
    public static implicit operator Money(decimal amount) => From(amount);
}