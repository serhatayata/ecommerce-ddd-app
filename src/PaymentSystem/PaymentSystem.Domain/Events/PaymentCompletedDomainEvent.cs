using Common.Domain.Events;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Domain.Events;

public sealed record PaymentCompletedDomainEvent : DomainEvent
{
    public Guid PaymentId { get; }
    public decimal Amount { get; }
    public PaymentMethod Method { get; }
    public Guid UserId { get; }
    public string TransactionId { get; }

    public PaymentCompletedDomainEvent(
        Guid paymentId,
        decimal amount,
        PaymentMethod method,
        Guid userId,
        string transactionId)
    {
        PaymentId = paymentId;
        Amount = amount;
        Method = method;
        UserId = userId;
        TransactionId = transactionId;
    }
}