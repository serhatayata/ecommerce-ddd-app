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
        Guid id,
        int userId,
        DateTime orderDate,
        OrderStatus status,
        decimal totalAmount,
        string shipmentDetail,
        Guid? correlationId = null)
        : base(correlationId)
    {
        Id = id;
        UserId = userId;
        OrderDate = orderDate;
        Status = status;
        TotalAmount = totalAmount;
        ShipmentDetail = shipmentDetail;
    }

    public Guid Id { get; init; }
    public int UserId { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
    public string ShipmentDetail { get; init; }
}