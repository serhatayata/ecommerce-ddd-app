using Common.Domain.Models;

namespace PaymentSystem.Domain.Models;

public class PaymentTransaction : Entity
{
    public PaymentTransaction()
    {
    }

    public PaymentTransaction(decimal amount, DateTime date, string transactionId)
    {
        Amount = amount;
        Date = date;
        TransactionId = transactionId;
    }

    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string TransactionId { get; private set; }
    public int PaymentId { get; private set; }

    public virtual Payment Payment { get; private set; }
}
