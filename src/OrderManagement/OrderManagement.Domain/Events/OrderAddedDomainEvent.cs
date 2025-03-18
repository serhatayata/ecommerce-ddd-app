using System.Text.Json.Serialization;
using Common.Domain.Events;
using MediatR;
using Order.Domain.DTOs;
using Order.Domain.Models.Orders;

namespace Order.Domain.Events;

public sealed record OrderAddedDomainEvent : DomainEvent, INotification
{
    [JsonConstructor]
    public OrderAddedDomainEvent()
    {
    }

    public OrderAddedDomainEvent(
        int orderId,
        Guid customerId,
        DateTime orderDate,
        OrderStatus status,
        decimal totalAmount,
        IReadOnlyList<OrderItemDto> items,
        Guid? correlationId = null) 
        : base(correlationId)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderDate = orderDate;
        Status = status;
        TotalAmount = totalAmount;
        Items = items;
    }

    public int OrderId { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime OrderDate { get; init; }
    public OrderStatus Status { get; init; }
    public decimal TotalAmount { get; init; }
    public IReadOnlyList<OrderItemDto> Items { get; init; }
}