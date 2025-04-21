using Common.Domain.Events.Stocks;
using MediatR;
using Stock.Domain.Events;

namespace Stock.Application.Events.Handlers;

public class StockAddedDomainEventHandler : INotificationHandler<StockAddedDomainEvent>
{
    private readonly IMediator _mediator;

    public StockAddedDomainEventHandler(
    IMediator mediator)
        => _mediator = mediator;

    public async Task Handle(
    StockAddedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var stockAddedEvent = new StockAddedEvent(
            notification.CorrelationId,
            notification.StockItemId,
            notification.AddedQuantity,
            notification.OccurredOn);

        await _mediator.Publish(stockAddedEvent, cancellationToken);
    }
}
