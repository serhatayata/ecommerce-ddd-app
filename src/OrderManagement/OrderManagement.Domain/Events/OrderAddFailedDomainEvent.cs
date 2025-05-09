using System.Text.Json.Serialization;
using Common.Domain.Events;
using MediatR;

namespace OrderManagement.Domain.Events;

public sealed record OrderAddFailedDomainEvent : DomainEvent, INotification
{
    [JsonConstructor]
    public OrderAddFailedDomainEvent()
    {
    }

    public OrderAddFailedDomainEvent(
        int? orderId,
        int userId,
        DateTime orderDate,
        decimal totalAmount,
        string errorMessage,
        Guid? correlationId = null) 
        : base(correlationId)
    {
        OrderId = orderId;
        UserId = userId;
        OrderDate = orderDate;
        TotalAmount = totalAmount;
        ErrorMessage = errorMessage;
    }

    public int UserId { get; init; }
    public int? OrderId { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal TotalAmount { get; init; }
    public string ErrorMessage { get; init; }
}