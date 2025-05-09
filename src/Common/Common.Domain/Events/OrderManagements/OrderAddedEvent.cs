using Common.Domain.ValueObjects;

namespace Common.Domain.Events.OrderManagements;

public sealed record OrderAddedEvent : IntegrationEvent
{
    public OrderAddedEvent(
        Guid? correlationId,
        int orderId,
        int userId,
        DateTime addedDate,
        OrderStatus status,
        decimal totalAmount) 
        : base(correlationId, addedDate)
    {
        OrderId = orderId;
        UserId = userId;
        OrderDate = addedDate;
        Status = status;
        TotalAmount = totalAmount;
    }

    public int OrderId { get; init; }
    public int UserId { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
}