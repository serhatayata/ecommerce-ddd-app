namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentCompletedEvent : IntegrationEvent
{
    public PaymentCompletedEvent()
    {
    }

    public PaymentCompletedEvent(
        Guid? correlationId,
        Guid orderId)
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
}