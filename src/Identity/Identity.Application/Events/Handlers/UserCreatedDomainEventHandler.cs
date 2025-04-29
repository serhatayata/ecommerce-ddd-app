using Common.Domain.Events.Identity;
using Identity.Domain.Events;
using MassTransit;
using MediatR;

namespace Identity.Application.Events.Handlers;

public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public UserCreatedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userCreatedEvent = new UserCreatedEvent(
            notification.CorrelationId,
            notification.UserId,
            notification.Email
        );

        await _publishEndpoint.Publish(userCreatedEvent, cancellationToken);
    }
}