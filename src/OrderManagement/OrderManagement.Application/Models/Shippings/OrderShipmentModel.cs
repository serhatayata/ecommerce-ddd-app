using Common.Domain.SharedKernel;
using Common.Domain.ValueObjects;

namespace OrderManagement.Application.Models.Shippings;

public class OrderShipmentModel
{
    public int OrderId { get; set; }
    public Address ShippingAddress { get; set; }
    public string TrackingNumber { get; set; }
    public int ShipmentCompanyId { get; set; }
    public ShipmentStatus Status { get; set; }

    public List<OrderShipmentItemModel> Items { get; set; }
}

public class OrderShipmentItemModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}