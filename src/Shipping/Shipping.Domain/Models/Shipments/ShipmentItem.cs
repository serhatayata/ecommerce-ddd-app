using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace Shipping.Domain.Models.Shipments;

public class ShipmentItem : Entity
{
    public ShipmentItem()
    {
    }

    public ShipmentItem(
    ProductId productId,
    int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public ShipmentId ShipmentId { get; private set; }
    public virtual Shipment Shipment { get; private set; }
}
