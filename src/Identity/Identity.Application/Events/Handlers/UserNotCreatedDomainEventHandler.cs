using Common.Domain.Events.Identity;
using Identity.Domain.Events;
using MassTransit;
using MediatR;

namespace Identity.Application.Events.Handlers;

public class UserNotCreatedDomainEventHandler : INotificationHandler<UserNotCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public UserNotCreatedDomainEventHandler(
    IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
    UserNotCreatedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var userNotCreatedEvent = new UserNotCreatedEvent(
            notification.CorrelationId,
            notification.Reason,
            notification.Email
        );

        await _publishEndpoint.Publish(userNotCreatedEvent, cancellationToken);
    }
}