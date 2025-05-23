using Common.Domain.Models;
using Common.Domain.ValueObjects;
using PaymentSystem.Domain.Events;

namespace PaymentSystem.Domain.Models;

public class Payment : Entity, IAggregateRoot
{
    public Payment()
    {
    }

    public Payment(
    int orderId, 
    decimal amount, 
    PaymentMethod method)
    {
        OrderId = orderId;
        Amount = Money.From(amount);
        Method = method;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        Transactions = new List<PaymentTransaction>();
    }

    public int OrderId { get; private set; }
    public Money Amount { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public virtual ICollection<PaymentTransaction> Transactions { get; private set; }

    public void MarkAsCompleted(Guid? correlationId = null)
    {
        Status = PaymentStatus.Completed;

        var paymentCompletedEvent = new PaymentCompletedDomainEvent(
            OrderId,
            Id,
            Amount.Amount,
            Method,
            correlationId
        );

        AddEvent(paymentCompletedEvent);
    }

    public void MarkAsFailed(Guid? correlationId = null)
    {
        Status = PaymentStatus.Failed;

        var paymentFailedEvent = new PaymentFailedDomainEvent(
            OrderId,
            Id,
            Amount.Amount,
            Method,
            "Payment failed",
            correlationId
        );

        AddEvent(paymentFailedEvent);
    }
}
