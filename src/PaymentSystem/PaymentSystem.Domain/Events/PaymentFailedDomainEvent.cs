using System.Text.Json.Serialization;
using Common.Domain.Events;
using Common.Domain.ValueObjects;

namespace PaymentSystem.Domain.Events;

public sealed record PaymentFailedDomainEvent : DomainEvent
{
    public int OrderId { get; }
    public int PaymentId { get; }
    public decimal Amount { get; }
    public PaymentMethod Method { get; }
    public string ErrorMessage { get; }

    [JsonConstructor]
    public PaymentFailedDomainEvent()
    {
    }

    public PaymentFailedDomainEvent(
        int orderId,
        int paymentId,
        decimal amount,
        PaymentMethod method,
        string errorMessage,
        Guid? correlationId)
        : base(correlationId)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        Amount = amount;
        Method = method;
        ErrorMessage = errorMessage;
    }
}