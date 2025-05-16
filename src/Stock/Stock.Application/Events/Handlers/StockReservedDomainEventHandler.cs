using MediatR;
using Stock.Domain.Events;
using Common.Domain.Events.Stocks;
using MassTransit;

namespace Stock.Application.Events.Handlers;

public class StockReservedDomainEventHandler : INotificationHandler<StocksReservedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StockReservedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        StocksReservedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var integrationEvent = new StocksReservedEvent(
            notification.CorrelationId,
            notification.OrderId,
            DateTime.UtcNow
        );

        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
