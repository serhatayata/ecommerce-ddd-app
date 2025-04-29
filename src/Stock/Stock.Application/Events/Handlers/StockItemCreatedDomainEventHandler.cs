using MediatR;
using Stock.Domain.Events;
using Common.Domain.Events.Stocks;
using MassTransit; // Add this

namespace Stock.Application.Events.Handlers;

public class StockItemCreatedDomainEventHandler : INotificationHandler<StockItemCreatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint; // Use MassTransit

    public StockItemCreatedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        StockItemCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new StockItemCreatedEvent(
            notification.StockItemId,
            notification.ProductId,
            notification.InitialQuantity,
            warehouse: notification.Warehouse,
            aisle: notification.Aisle,
            shelf: notification.Shelf,
            bin: notification.Bin,
            createdDate: notification.CreatedDate,
            notification.CorrelationId
        );

        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
