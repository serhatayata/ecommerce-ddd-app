using System.Text.Json.Serialization;
using Common.Domain.Events;
using Common.Domain.ValueObjects;
using MediatR;

namespace OrderManagement.Domain.Events;

public sealed record OrderAddedDomainEvent : DomainEvent, INotification
{
    [JsonConstructor]
    public OrderAddedDomainEvent()
    {
    }

    public OrderAddedDomainEvent(
        int orderId,
        int userId,
        DateTime orderDate,
        OrderStatus status,
        decimal totalAmount,
        Guid? correlationId = null)
        : base(correlationId)
    {
        OrderId = orderId;
        UserId = userId;
        OrderDate = orderDate;
        Status = status;
        TotalAmount = totalAmount;
    }

    public int OrderId { get; init; }
    public int UserId { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
}