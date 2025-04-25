using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace PaymentSystem.Domain.Models;

public class Payment : Entity, IAggregateRoot
{
    public Payment(decimal amount, PaymentMethod method)
    {
        Amount = Money.From(amount);
        Method = method;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        Transactions = new List<PaymentTransaction>();
    }

    public Money Amount { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public virtual ICollection<PaymentTransaction> Transactions { get; private set; }

    public void MarkAsCompleted()
    {
        Status = PaymentStatus.Completed;
    }

    public void MarkAsFailed()
    {
        Status = PaymentStatus.Failed;
    }
}
