namespace Common.Domain.ValueObjects;

public sealed class UserId : ValueObject
{
    public int Value { get; }

    private UserId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("User ID must be a positive number", nameof(value));
            
        Value = value;
    }

    public static UserId From(int value) => new(value);

    public static implicit operator int(UserId userId) => userId.Value;
    public static implicit operator UserId(int value) => From(value);

    public override string ToString() => Value.ToString();
}