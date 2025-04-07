using Common.Application.Extensions;
using MassTransit;
using MediatR;
using Stock.Domain.Events;

namespace Stock.Application.Events.Handlers;

public class StockReservedDomainEventHandler : INotificationHandler<StockReservedDomainEvent>
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public StockReservedDomainEventHandler(
    ISendEndpointProvider sendEndpointProvider)
        => _sendEndpointProvider = sendEndpointProvider;

    public async Task Handle(
    StockReservedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        // Use Stock reserve command for business here

        // Create StockReservedIntegrationEvent here

        var queueName = "";

        ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

        await sendEndpoint.Send(notification, cancellationToken);
    }
}
