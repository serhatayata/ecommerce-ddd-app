using MassTransit;

namespace Common.Domain.Models;

public interface IDomainEvent : CorrelatedBy<Guid>
{
}