using Common.Domain.Models;

namespace Common.Infrastructure.Events;

public interface IEventDispatcher
{
    Task Dispatch(IDomainEvent domainEvent);
}