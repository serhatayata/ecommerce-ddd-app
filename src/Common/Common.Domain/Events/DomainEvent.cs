namespace Common.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid CorrelationId { get; set; }
}