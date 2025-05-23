using Common.Domain.Models;
using Common.Domain.ValueObjects;
using OrderManagement.Domain.Events;

namespace OrderManagement.Domain.Models.Orders;

public class Order : Entity, IAggregateRoot
{
    public HashSet<OrderItem> OrderItems { get; private set; }

    private Order(
    UserId userId,
    DateTime orderDate)
    {
        UserId = userId;
        OrderDate = orderDate;
        OrderItems = new HashSet<OrderItem>();
        Status = OrderStatus.Pending;
    }

    public static Order Create(
    UserId userId,
    DateTime orderDate)
        => new(userId, orderDate);

    public UserId UserId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }

    public Order UpdateStatus(OrderStatus status)
    {
        Status = status;
        return this;
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        OrderItems.Add(orderItem);
    }

    public void RaiseOrderCreatedEvent()
    {
        decimal totalAmount = OrderItems?.Sum(x => x.UnitPrice?.Amount ?? 0) ?? 0;

        AddEvent(new OrderAddedDomainEvent(
            Id,
            UserId.Value,
            OrderDate,
            Status,
            totalAmount
        ));
    }
}