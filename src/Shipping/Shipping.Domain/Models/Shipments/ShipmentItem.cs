using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace Shipping.Domain.Models.Shipments;

public class ShipmentItem : Entity
{
    private ShipmentItem()
    {
    }

    private ShipmentItem(
    ProductId productId,
    int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public static ShipmentItem Create(
    ProductId productId,
    int quantity)
        => new ShipmentItem(productId, quantity);

    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public int ShipmentId { get; private set; }
    public virtual Shipment Shipment { get; private set; }
}
