using Common.Domain.Models;
using Common.Domain.ValueObjects;
using PaymentSystem.Domain.Events;

namespace PaymentSystem.Domain.Models;

public class Payment : Entity, IAggregateRoot
{
    private Payment()
    {
    }

    private Payment(
    OrderId orderId, 
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

    public static Payment Create(
    OrderId orderId,
    decimal amount,
    PaymentMethod method)
        => new Payment(orderId, amount, method);

    public OrderId OrderId { get; private set; }
    public Money Amount { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public virtual ICollection<PaymentTransaction> Transactions { get; private set; }

    public void RaisePaymentCompletedEvent(Guid? correlationId = null)
    {
        Status = PaymentStatus.Completed;

        var paymentCompletedEvent = new PaymentCompletedDomainEvent(
            OrderId.Value,
            correlationId
        );

        AddEvent(paymentCompletedEvent);
    }
}
