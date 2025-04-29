using MediatR;
using Stock.Domain.Events;
using Common.Domain.Events.Stocks;
using MassTransit;

namespace Stock.Application.Events.Handlers;

public class StockReservedDomainEventHandler : INotificationHandler<StockReservedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StockReservedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        StockReservedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var integrationEvent = new StockReservedEvent(
            notification.CorrelationId,
            notification.StockItemId,
            notification.OrderId,
            notification.Quantity,
            DateTime.UtcNow
        );

        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
