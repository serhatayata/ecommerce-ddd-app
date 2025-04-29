using Common.Domain.Events.PaymentSystems;
using MediatR;
using PaymentSystem.Domain.Events;
using MassTransit; // Add this using

namespace PaymentSystem.Application.Events.Handlers;

public class PaymentFailedDomainEventHandler : INotificationHandler<PaymentFailedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PaymentFailedDomainEventHandler(IPublishEndpoint publishEndpoint)
        => _publishEndpoint = publishEndpoint;

    public async Task Handle(
        PaymentFailedDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var paymentFailedEvent = new PaymentFailedEvent(
            notification.CorrelationId,
            notification.OrderId,
            notification.PaymentId,
            notification.Amount);

        await _publishEndpoint.Publish(paymentFailedEvent, cancellationToken);
    }
}