using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace PaymentSystem.Domain.Models;

public class PaymentTransaction : Entity
{
    private PaymentTransaction()
    {
    }

    private PaymentTransaction(
    decimal amount,
    DateTime date,
    string transactionId,
    PaymentStatus status)
    {
        Amount = amount;
        Date = date;
        TransactionId = transactionId;
        Status = status;
    }

    public static PaymentTransaction Create(
        decimal amount,
        DateTime date,
        string transactionId,
        PaymentStatus status)
        => new PaymentTransaction(amount, date, transactionId, status);

    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string TransactionId { get; private set; }
    public int PaymentId { get; private set; }
    public PaymentStatus Status { get; private set; }

    public virtual Payment Payment { get; private set; }
}
