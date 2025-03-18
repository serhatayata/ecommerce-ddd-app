using Common.Domain.Models;

namespace OrderManagement.Domain.Models.Orders;

public class OrderItem : Entity
{
    internal OrderItem(
    Guid orderId, 
    Guid productId, 
    int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem UpdateProductId(Guid productId)
    {
        ProductId = productId;
        return this;
    }

    public OrderItem UpdateQuantity(int quantity)
    {
        Quantity = quantity;
        return this;
    }
}