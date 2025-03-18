using Common.Domain.Models;
using OrderManagement.Domain.Events;

namespace OrderManagement.Domain.Models.Orders;

public class Order : Entity, IAggregateRoot
{
    public HashSet<OrderItem> OrderItems { get; private set; }

    public Order(
    Guid customerId, 
    DateTime orderDate)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
        OrderItems = new HashSet<OrderItem>();
        Status = OrderStatus.Pending;
        
        AddEvent(new OrderAddedDomainEvent());
    }

    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }

    public Order UpdateStatus(OrderStatus status)
    {
        Status = status;
        return this;
    }
}