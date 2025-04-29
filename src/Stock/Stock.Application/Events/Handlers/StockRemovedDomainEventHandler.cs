using MediatR;
using Stock.Domain.Events;
using Common.Domain.Events.Stocks;
using MassTransit;

namespace Stock.Application.Events.Handlers;

public class StockRemovedDomainEventHandler : INotificationHandler<StockRemovedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StockRemovedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        StockRemovedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var integrationEvent = new StockRemovedEvent(
            notification.CorrelationId,
            notification.StockItemId,
            notification.Quantity,
            notification.RemovedDate
        );

        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
