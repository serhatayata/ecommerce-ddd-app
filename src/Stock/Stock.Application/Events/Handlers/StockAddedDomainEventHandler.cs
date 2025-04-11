using Common.Application.Extensions;
using MassTransit;
using MediatR;
using Stock.Domain.Events;

namespace Stock.Application.Events.Handlers;

public class StockAddedDomainEventHandler : INotificationHandler<StockAddedDomainEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public StockAddedDomainEventHandler(
    ISendEndpointProvider sendEndpointProvider)
        => _sendEndpointProvider = sendEndpointProvider;

    public async Task Handle(
    StockAddedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        // Create StockAddedIntegrationEvent here

        string queueName = "";

        ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

        await sendEndpoint.Send(notification, cancellationToken);
    }
}
