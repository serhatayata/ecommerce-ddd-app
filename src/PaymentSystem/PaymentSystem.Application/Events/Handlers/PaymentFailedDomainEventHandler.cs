using Common.Domain.Events.PaymentSystems;
using MediatR;
using PaymentSystem.Domain.Events;

namespace PaymentSystem.Application.Events.Handlers;

public class PaymentFailedDomainEventHandler : INotificationHandler<PaymentFailedDomainEvent>
{
    private readonly IMediator _mediator;

    public PaymentFailedDomainEventHandler(
    IMediator mediator)
        => _mediator = mediator;

    public async Task Handle(
    PaymentFailedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var paymentFailedEvent = new PaymentFailedEvent(
            notification.CorrelationId,
            notification.OrderId,
            notification.PaymentId,
            notification.Amount);

        await _mediator.Publish(paymentFailedEvent, cancellationToken);    }
}