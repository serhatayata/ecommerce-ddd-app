using Common.Domain.Events.PaymentSystems;
using MediatR;
using PaymentSystem.Domain.Events;
using MassTransit; // Add this using

namespace PaymentSystem.Application.Events.Handlers;

public class PaymentCompletedDomainEventHandler : INotificationHandler<PaymentCompletedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PaymentCompletedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        PaymentCompletedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var paymentCompletedEvent = new PaymentCompletedEvent(
            notification.CorrelationId,
            notification.OrderId,
            notification.PaymentId,
            notification.Amount);

        await _publishEndpoint.Publish(paymentCompletedEvent, cancellationToken);
    }
}