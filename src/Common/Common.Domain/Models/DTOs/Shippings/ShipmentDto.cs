using Common.Domain.SharedKernel;
using Common.Domain.ValueObjects;

namespace Common.Domain.Models.DTOs.Shippings;

public class ShipmentDto
{
    public Guid OrderId { get; set; }
    public AddressDto ShippingAddress { get; set; }
    public string TrackingNumber { get; set; }
    public int ShipmentCompanyId { get; set; }
    public ShipmentStatus Status { get; set; }

    public List<ShipmentItemDto> Items { get; set; }
}

public class ShipmentItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}