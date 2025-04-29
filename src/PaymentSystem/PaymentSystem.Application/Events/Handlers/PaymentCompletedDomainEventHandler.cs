using Common.Domain.Events.PaymentSystems;
using MediatR;
using PaymentSystem.Domain.Events;

namespace PaymentSystem.Application.Events.Handlers;

public class PaymentCompletedDomainEventHandler : INotificationHandler<PaymentCompletedDomainEvent>
{
    private readonly IMediator _mediator;

    public PaymentCompletedDomainEventHandler(
    IMediator mediator)
        => _mediator = mediator;

    public async Task Handle(
    PaymentCompletedDomainEvent notification, 
    CancellationToken cancellationToken)
    {
        var paymentCompletedEvent = new PaymentCompletedEvent(
            notification.CorrelationId,
            notification.PaymentId,
            notification.OrderId,
            notification.Amount);

        await _mediator.Publish(paymentCompletedEvent, cancellationToken);
    }
}