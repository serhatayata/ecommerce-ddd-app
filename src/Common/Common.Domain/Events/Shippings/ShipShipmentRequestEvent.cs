using Common.Domain.Models.DTOs.Shippings;

namespace Common.Domain.Events.Shippings;

public sealed record ShipShipmentRequestEvent : IntegrationEvent
{
    public ShipShipmentRequestEvent()
    {
    }

    public ShipShipmentRequestEvent(
        Guid correlationId,
        Guid orderId,
        ShipmentDto shipmentDetail,
        DateTime shippedDate) :
    base(correlationId, DateTime.UtcNow)
    {
        ShippedDate = shippedDate;
        OrderId = orderId;
        ShipmentDetail = shipmentDetail;
    }

    public Guid OrderId { get; set; }
    public DateTime ShippedDate { get; set; }
    public ShipmentDto ShipmentDetail { get; set; }
}