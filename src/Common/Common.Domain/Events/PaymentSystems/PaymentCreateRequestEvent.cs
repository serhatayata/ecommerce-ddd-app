namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentCreateRequestEvent : IntegrationEvent
{
    public PaymentCreateRequestEvent()
    {   
    }

    public PaymentCreateRequestEvent(
        Guid? correlationId,
        int orderId)
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
    }

    public int OrderId { get; set; }
}