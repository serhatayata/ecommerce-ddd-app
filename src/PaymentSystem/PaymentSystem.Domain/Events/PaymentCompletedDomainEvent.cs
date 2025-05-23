using System.Text.Json.Serialization;
using Common.Domain.Events;
using Common.Domain.ValueObjects;

namespace PaymentSystem.Domain.Events;

public sealed record PaymentCompletedDomainEvent : DomainEvent
{
    public int OrderId { get; }
    public int PaymentId { get; }
    public decimal Amount { get; }
    public PaymentMethod Method { get; }

    [JsonConstructor]
    public PaymentCompletedDomainEvent()
    {
    }

    public PaymentCompletedDomainEvent(
        int orderId,
        int paymentId,
        decimal amount,
        PaymentMethod method,
        Guid? correlationId)
        : base(correlationId)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        Amount = amount;
        Method = method;
    }
}