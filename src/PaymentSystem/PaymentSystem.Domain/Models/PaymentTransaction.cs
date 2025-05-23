using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace PaymentSystem.Domain.Models;

public class PaymentTransaction : Entity
{
    public PaymentTransaction()
    {
    }

    public PaymentTransaction(decimal amount, DateTime date, string transactionId, PaymentStatus status)
    {
        Amount = amount;
        Date = date;
        TransactionId = transactionId;
        Status = status;
    }

    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string TransactionId { get; private set; }
    public PaymentId PaymentId { get; private set; }
    public PaymentStatus Status { get; private set; }

    public virtual Payment Payment { get; private set; }
}
