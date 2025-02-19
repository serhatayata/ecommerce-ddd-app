namespace Common.Domain.Models;

public interface IEntity
{   
    IReadOnlyCollection<IDomainEvent> Events { get; }
    void ClearEvents();
}