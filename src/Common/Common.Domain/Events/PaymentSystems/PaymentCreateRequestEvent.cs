using Common.Domain.ValueObjects;

namespace Common.Domain.Events.PaymentSystems;

public sealed record PaymentCreateRequestEvent : IntegrationEvent
{
    public PaymentCreateRequestEvent(
        Guid? correlationId,
        int orderId,
        decimal amount,
        PaymentMethod method) 
        : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        Amount = amount;
        Method = method;
    }

    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
}