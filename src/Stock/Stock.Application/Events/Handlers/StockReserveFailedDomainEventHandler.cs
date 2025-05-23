using Common.Domain.Events.Stocks;
using MassTransit;
using MediatR;
using Stock.Domain.Events;

namespace Stock.Application.Events.Handlers;

public class StockReserveFailedDomainEventHandler : INotificationHandler<StockReserveFailedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public StockReserveFailedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
    StockReserveFailedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var integrationEvent = new StockReserveFailedEvent(
            notification.CorrelationId,
            notification.OrderId,
            DateTime.UtcNow,
            notification.ErrorMessage
        );

        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}