using MediatR;
using Stock.Domain.Events;
using Common.Domain.Events.Stocks;

namespace Stock.Application.Events.Handlers;

public class StockRemovedDomainEventHandler : INotificationHandler<StockRemovedDomainEvent>
{
    private readonly IMediator _mediator;

    public StockRemovedDomainEventHandler(
        IMediator mediator)
        => _mediator = mediator;

    public async Task Handle(
        StockRemovedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var integrationEvent = new StockRemovedEvent(
            notification.CorrelationId,
            notification.StockItemId,
            notification.RemovedQuantity,
            DateTime.UtcNow
        );

        await _mediator.Publish(integrationEvent, cancellationToken);
    }
}
