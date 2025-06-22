using Common.Domain.Models;
using Common.Domain.ValueObjects;
using OrderManagement.Domain.Events;

namespace OrderManagement.Domain.Models.Orders;

public class Order : Entity<Guid>, IAggregateRoot
{
    public HashSet<OrderItem> OrderItems { get; private set; }

    private Order()
    {
        Id = Guid.NewGuid();
        OrderItems = new HashSet<OrderItem>();
    }

    private Order(
        UserId userId,
        DateTime orderDate)
    {
        Id = Guid.NewGuid();
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

    public void RaiseOrderCreatedEvent(string shipmentDetails)
    {
        decimal totalAmount = OrderItems?.Sum(x => x.UnitPrice?.Amount ?? 0) ?? 0;

        AddEvent(new OrderAddedDomainEvent(
            Id,
            UserId.Value,
            OrderDate,
            Status,
            totalAmount,
            shipmentDetails,
            Guid.NewGuid()
        ));
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Order other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (Id.Equals(default) || other.Id.Equals(default)) return false;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();
}