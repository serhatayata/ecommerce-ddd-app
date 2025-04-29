using Common.Domain.Models;
using Shipping.Domain.Events;

namespace Shipping.Domain.Models.Shipments;

public class Shipment : Entity, IAggregateRoot
{
    private readonly List<ShipmentItem> _items = new();

    public Shipment()
    {
    }

    public Shipment(
        int orderId,
        Address shippingAddress,
        string trackingNumber,
        int shipmentCompanyId)
    {
        OrderId = orderId;
        ShippingAddress = shippingAddress;
        TrackingNumber = trackingNumber;
        ShipmentCompanyId = shipmentCompanyId;
        Status = ShipmentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public int OrderId { get; private set; }
    public Address ShippingAddress { get; private set; }
    public string TrackingNumber { get; private set; }
    public int ShipmentCompanyId { get; set; }
    public ShipmentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ShippedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }

    public ShipmentCompany ShipmentCompany { get; private set; }
    public virtual IReadOnlyCollection<ShipmentItem> Items => _items.AsReadOnly();

    public void Ship()
    {
        Status = ShipmentStatus.Shipped;
        ShippedAt = DateTime.UtcNow;
    }
    
    public void UpdateStatus(
    ShipmentStatus newStatus, 
    Guid? correlationId = null)
    {
        Status = newStatus;

        switch (newStatus)
        {
            case ShipmentStatus.Shipped:
                ShippedAt = DateTime.UtcNow;
                AddEvent(new ShipmentShippedDomainEvent(
                    Id,
                    TrackingNumber,
                    ShippedAt.Value,
                    correlationId
                ));
                break;
            case ShipmentStatus.Delivered:
                DeliveredAt = DateTime.UtcNow;
                AddEvent(new ShipmentDeliveredDomainEvent(
                    Id,
                    TrackingNumber,
                    DeliveredAt.Value,
                    correlationId
                ));
                break;
        }
    }

    public void UpdateShipmentCompany(int shipmentCompanyId)
        => ShipmentCompanyId = shipmentCompanyId;

    public void AddItem(int productId, int quantity)
        => _items.Add(new ShipmentItem(productId, quantity));
}
