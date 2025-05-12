using Common.Domain.Models;
using Common.Domain.ValueObjects;
using OrderManagement.Domain.Events;

namespace OrderManagement.Domain.Models.Orders;

public class Order : Entity, IAggregateRoot
{
    public HashSet<OrderItem> OrderItems { get; private set; }

    public Order(
    int userId, 
    DateTime orderDate)
    {
        UserId = userId;
        OrderDate = orderDate;
        OrderItems = new HashSet<OrderItem>();
        Status = OrderStatus.Pending;

        decimal totalAmount = OrderItems?.Sum(x => x.UnitPrice?.Amount ?? 0) ?? 0;

        AddEvent(new OrderAddedDomainEvent(
            Id,
            UserId,
            OrderDate,
            Status,
            totalAmount
        ));
    }

    public int UserId { get; private set; }
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
}