using Common.Domain.Models;
using MediatR;

namespace Common.Infrastructure.Events;

internal class EventDispatcher : IEventDispatcher
{
    private readonly IPublisher _publisher;

    public EventDispatcher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Dispatch(IDomainEvent domainEvent)
    {
        await _publisher.Publish(domainEvent);
    }
}