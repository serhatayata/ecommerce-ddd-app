namespace Common.Domain.SharedKernel;

public abstract class TypedIdValueBase<T, TValue> : IEquatable<TypedIdValueBase<T, TValue>>
    where T : TypedIdValueBase<T, TValue>
    where TValue : IEquatable<TValue>
{
    public TValue Value { get; }

    protected TypedIdValueBase(TValue value)
    {
        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is T other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }

    public bool Equals(TypedIdValueBase<T, TValue> other)
    {
        if (ReferenceEquals(null, other)) return false;
        return Value.Equals(other.Value);
    }

    public static bool operator ==(TypedIdValueBase<T, TValue> obj1, TypedIdValueBase<T, TValue> obj2)
    {
        if (ReferenceEquals(obj1, null))
        {
            return ReferenceEquals(obj2, null);
        }
        return obj1.Equals(obj2);
    }

    public static bool operator !=(TypedIdValueBase<T, TValue> x, TypedIdValueBase<T, TValue> y) 
    {
        return !(x == y);
    }

    public override string ToString() => Value?.ToString() ?? string.Empty;
}