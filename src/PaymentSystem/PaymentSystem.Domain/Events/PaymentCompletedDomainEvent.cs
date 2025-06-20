using System.Text.Json.Serialization;
using Common.Domain.Events;
using MediatR;

namespace PaymentSystem.Domain.Events;

public sealed record PaymentCompletedDomainEvent : DomainEvent, INotification
{
    public Guid OrderId { get; }

    [JsonConstructor]
    public PaymentCompletedDomainEvent()
    {
    }

    public PaymentCompletedDomainEvent(
        Guid orderId,
        Guid? correlationId)
        : base(correlationId)
    {
        OrderId = orderId;
    }
}