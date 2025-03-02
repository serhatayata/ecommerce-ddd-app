namespace Common.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    protected DomainEvent(Guid? correlationId = null)
    {
        CorrelationId = correlationId ?? Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    public Guid CorrelationId { get; init; }
    public DateTime OccurredOn { get; init; }
}