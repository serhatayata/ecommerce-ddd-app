namespace Common.Domain.Events;

public interface IDomainEvent
{
    public Guid CorrelationId { get; init; }
}