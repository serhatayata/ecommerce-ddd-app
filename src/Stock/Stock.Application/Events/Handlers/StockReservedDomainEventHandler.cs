using MediatR;
using Stock.Domain.Events;
using Common.Domain.Events.Stocks;

namespace Stock.Application.Events.Handlers;

public class StockReservedDomainEventHandler : INotificationHandler<StockReservedDomainEvent>
{
    private readonly IMediator _mediator;

    public StockReservedDomainEventHandler(IMediator mediator)
        => _mediator = mediator;

    public async Task Handle(
        StockReservedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var integrationEvent = new StockReservedEvent(
            notification.CorrelationId,
            notification.StockItemId,
            notification.OrderId,
            notification.ReservedQuantity,
            DateTime.UtcNow
        );

        await _mediator.Publish(integrationEvent, cancellationToken);
    }
}
