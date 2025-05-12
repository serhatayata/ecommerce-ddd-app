namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentFailedEvent : IntegrationEvent
{
    public PaymentFailedEvent(
        Guid? correlationId,
        int orderId,
        int paymentId,
        decimal amount,
        string errorMessage) 
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        PaymentId = paymentId;
        Amount = amount;
        ErrorMessage = errorMessage;
    }

    public int OrderId { get; set; }
    public int PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string ErrorMessage { get; set; }
}