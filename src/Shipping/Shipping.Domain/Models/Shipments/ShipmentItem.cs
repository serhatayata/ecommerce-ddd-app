using Common.Domain.Models;

namespace Shipping.Domain.Models.Shipments;

public class ShipmentItem : Entity
{
    public ShipmentItem(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public int ShipmentId { get; private set; }
    public virtual Shipment Shipment { get; private set; }
}
