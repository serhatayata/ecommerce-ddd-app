using Common.Domain.Events;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Domain.Events;

public sealed record PaymentFailedDomainEvent : DomainEvent
{
    public Guid PaymentId { get; }
    public decimal Amount { get; }
    public PaymentMethod Method { get; }
    public Guid UserId { get; }
    public string ErrorMessage { get; }

    public PaymentFailedDomainEvent(
        Guid paymentId,
        decimal amount,
        PaymentMethod method,
        Guid userId,
        string errorMessage)
    {
        PaymentId = paymentId;
        Amount = amount;
        Method = method;
        UserId = userId;
        ErrorMessage = errorMessage;
    }
}