using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace OrderManagement.Domain.Models.Orders;

public class OrderItem : Entity
{
    private OrderItem()
    {
    }

    private OrderItem(
    int orderId, 
    int productId, 
    int quantity,
    Money unitPrice)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static OrderItem Create(
        int orderId,
        int productId,
        int quantity,
        Money unitPrice)
        => new OrderItem(orderId, productId, quantity, unitPrice);

    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    public OrderItem UpdateProductId(int productId)
    {
        ProductId = productId;
        return this;
    }

    public OrderItem UpdateQuantity(int quantity)
    {
        Quantity = quantity;
        return this;
    }

    public OrderItem UpdateUnitPrice(Money unitPrice)
    {
        UnitPrice = unitPrice;
        return this;
    }
}