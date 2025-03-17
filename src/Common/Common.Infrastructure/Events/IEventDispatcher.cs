using Common.Domain.Events;

namespace Common.Infrastructure.Events;

public interface IEventDispatcher
{
    Task Dispatch(IDomainEvent domainEvent);
}