using MediatR;

namespace Common.Domain.Events;

public interface IDomainEvent : INotification
{
    public Guid CorrelationId { get; init; }
}