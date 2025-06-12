using System.Text.Json.Serialization;
using Common.Domain.Events;

namespace PaymentSystem.Domain.Events;

public sealed record PaymentCompletedDomainEvent : DomainEvent
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