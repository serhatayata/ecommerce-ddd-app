namespace Shipping.Domain.Models.Shipments;

public enum ShipmentStatus
{
    None = 0,
    Pending = 1,
    Shipped = 2,
    Delivered = 3,
    Failed = 4
}
