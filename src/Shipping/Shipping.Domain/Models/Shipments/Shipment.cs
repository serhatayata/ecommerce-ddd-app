using Common.Domain.Models;
using Common.Domain.ValueObjects;
using Shipping.Domain.Events;

namespace Shipping.Domain.Models.Shipments;

public class Shipment : Entity, IAggregateRoot
{
    private readonly List<ShipmentItem> _items = new();

    private Shipment()
    {
    }

    private Shipment(
        OrderId orderId,
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

    public static Shipment Create(
    OrderId orderId,
    Address shippingAddress,
    string trackingNumber,
    int shipmentCompanyId)
        => new Shipment(orderId,
                        shippingAddress,
                        trackingNumber,
                        shipmentCompanyId);

    public OrderId OrderId { get; private set; }
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
                RaiseShipmentShippedDomainEvent(correlationId);
                break;
            case ShipmentStatus.Delivered:
                DeliveredAt = DateTime.UtcNow;
                RaiseShipmentDeliveredDomainEvent(correlationId);
                break;
        }
    }

    public void RaiseShipmentShippedDomainEvent(Guid? correlationId = null)
        => AddEvent(new ShipmentShippedDomainEvent(
            Id,
            TrackingNumber,
            ShippedAt.Value,
            correlationId
        ));

    public void RaiseShipmentDeliveredDomainEvent(Guid? correlationId = null)
        => AddEvent(new ShipmentDeliveredDomainEvent(
            OrderId.Value,
            Id,
            TrackingNumber,
            DeliveredAt.Value,
            correlationId
        ));

    public void UpdateShipmentCompany(int shipmentCompanyId)
        => ShipmentCompanyId = shipmentCompanyId;

    public void AddItem(ProductId productId, int quantity)
        => _items.Add(ShipmentItem.Create(productId, quantity));
}
