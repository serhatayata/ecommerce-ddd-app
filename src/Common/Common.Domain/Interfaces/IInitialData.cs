namespace Common.Domain.Interfaces;

public interface IInitialData
{
    Type EntityType { get; }

    IEnumerable<object> GetData();
}