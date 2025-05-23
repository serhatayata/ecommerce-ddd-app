using Common.Domain.ValueObjects;

namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentCompletedEvent : IntegrationEvent
{
    public PaymentCompletedEvent(
        Guid? correlationId,
        int orderId,
        int paymentId,
        decimal amount) 
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        Amount = amount;
    }

    public int OrderId { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}