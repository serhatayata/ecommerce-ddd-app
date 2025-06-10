namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentCompletedEvent : IntegrationEvent
{
    public PaymentCompletedEvent(
        Guid? correlationId,
        Guid orderId,
        int paymentId,
        decimal amount) 
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        Amount = amount;
    }

    public Guid OrderId { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
}