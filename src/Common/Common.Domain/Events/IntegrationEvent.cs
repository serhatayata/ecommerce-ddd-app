using System.Text.Json.Serialization;
using MassTransit;
using MediatR;

namespace Common.Domain.Events;

public abstract record IntegrationEvent : INotification, CorrelatedBy<Guid>
{
    [JsonConstructor]
    protected IntegrationEvent()
    {
    }

    protected IntegrationEvent(
    Guid? correlationId,
    DateTime createDate)
    {
        CorrelationId = correlationId ?? Guid.NewGuid();
        CreationDate = createDate;
    }

    public Guid CorrelationId { get; init; }
    public DateTime CreationDate { get; init; }
}