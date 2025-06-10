namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentCreateRequestEvent : IntegrationEvent
{
    public PaymentCreateRequestEvent()
    {   
    }

    public PaymentCreateRequestEvent(
        Guid? correlationId,
        Guid orderId)
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; set; }
}