using Common.Application.Extensions;
using Identity.Domain.Events;
using MassTransit;
using MediatR;

namespace Identity.Application.Events.Handlers;

public class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public UserCreatedDomainEventHandler(
    ISendEndpointProvider sendEndpointProvider)
        => _sendEndpointProvider = sendEndpointProvider;

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var eventType = notification.GetType();
        var queueName = MessageBrokerExtensions.GetQueueName(eventType);

        ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

        await sendEndpoint.Send(notification, cancellationToken);
    }
}