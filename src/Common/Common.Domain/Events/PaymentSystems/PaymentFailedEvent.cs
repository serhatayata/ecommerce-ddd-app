namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentFailedEvent : IntegrationEvent
{
    public PaymentFailedEvent(
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