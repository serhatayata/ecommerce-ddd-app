using Common.Application.Extensions;
using MassTransit;
using MediatR;
using Stock.Domain.Events;

namespace Stock.Application.Events.Handlers;

public class StockItemCreatedDomainEventHandler : INotificationHandler<StockItemCreatedDomainEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public StockItemCreatedDomainEventHandler(
    ISendEndpointProvider sendEndpointProvider)
        => _sendEndpointProvider = sendEndpointProvider;

    public async Task Handle(
    StockItemCreatedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        // Use StockItem create command for business here

        // Create StockItemCreatedIntegrationEvent

        string queueName = "";

        ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

        await sendEndpoint.Send(notification, cancellationToken);
    }
}
