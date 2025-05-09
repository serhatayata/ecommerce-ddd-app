namespace Common.Domain.Events.OrderManagements;

public sealed record OrderAddFailedEvent : IntegrationEvent
{
    public OrderAddFailedEvent(
        Guid? correlationId,
        int? orderId,
        int userId,
        DateTime addedDate,
        string errorMessage) 
        : base(correlationId, addedDate)
    {
        OrderId = orderId;
        UserId = userId;
        OrderDate = addedDate;
        ErrorMessage = errorMessage;
    }

    public int? OrderId { get; init; }
    public int UserId { get; init; }
    public DateTime OrderDate { get; init; }
    public string ErrorMessage { get; init; }
}