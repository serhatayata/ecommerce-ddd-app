using Common.Domain.Models;

namespace OrderManagement.Domain.Models.Orders;

public class OrderItem : Entity
{
    public OrderItem(
    int orderId, 
    int productId, 
    int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

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
}