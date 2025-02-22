namespace Common.Domain.Events;

public abstract record IntegrationEvent
{
    protected IntegrationEvent()
    {
        CorrelationId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    protected IntegrationEvent(
    Guid correlationId, 
    DateTime createDate)
    {
        CorrelationId = correlationId;
        CreationDate = createDate;
    }

    public Guid CorrelationId { get; private init; }
    public DateTime CreationDate { get; private init; }
}