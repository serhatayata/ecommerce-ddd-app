using Common.Domain.Events.Stocks;
using MediatR;
using Stock.Domain.Events;
using MassTransit;

namespace Stock.Application.Events.Handlers;

public class StockAddedDomainEventHandler : INotificationHandler<StockAddedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StockAddedDomainEventHandler(
        IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        StockAddedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var stockAddedEvent = new StockAddedEvent(
            notification.CorrelationId,
            notification.StockItemId,
            notification.AddedQuantity,
            notification.OccurredOn);

        await _publishEndpoint.Publish(stockAddedEvent, cancellationToken);
    }
}
