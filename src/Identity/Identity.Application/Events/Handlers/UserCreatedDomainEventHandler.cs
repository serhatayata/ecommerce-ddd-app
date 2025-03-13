using Identity.Domain.Events;
using MediatR;

namespace Identity.Application.Events.Handlers;

public sealed record UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        BURADAN DEVAM
        throw new NotImplementedException();
    }
}