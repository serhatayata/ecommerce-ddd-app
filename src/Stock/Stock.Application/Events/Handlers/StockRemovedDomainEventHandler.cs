using Common.Application.Extensions;
using MassTransit;
using MediatR;
using Stock.Domain.Events;

namespace Stock.Application.Events.Handlers;

public class StockRemovedDomainEventHandler : INotificationHandler<StockRemovedDomainEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public StockRemovedDomainEventHandler(
    ISendEndpointProvider sendEndpointProvider)
        => _sendEndpointProvider = sendEndpointProvider;

    public async Task Handle(
    StockRemovedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        // Create StockRemovedIntegrationEvent here

        string queueName = "";

        ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

        await sendEndpoint.Send(notification, cancellationToken);
    }
}
