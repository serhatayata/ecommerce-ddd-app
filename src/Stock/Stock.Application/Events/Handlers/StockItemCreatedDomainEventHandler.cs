using MediatR;
using Stock.Domain.Events;
using Common.Domain.Events.Stocks;

namespace Stock.Application.Events.Handlers;

public class StockItemCreatedDomainEventHandler : INotificationHandler<StockItemCreatedDomainEvent>
{
    private readonly IMediator _mediator;

    public StockItemCreatedDomainEventHandler(IMediator mediator)
        => _mediator = mediator;

    public async Task Handle(
        StockItemCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var integrationEvent = new StockItemCreatedEvent(
            notification.CorrelationId,
            notification.ProductId,
            notification.InitialQuantity,
            warehouse: "", // Gerekirse doldurun
            aisle: "",
            shelf: "",
            bin: "",
            createdDate: DateTime.UtcNow
        );

        await _mediator.Publish(integrationEvent, cancellationToken);
    }
}
