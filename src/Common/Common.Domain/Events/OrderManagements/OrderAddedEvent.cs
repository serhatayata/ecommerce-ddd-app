using Common.Domain.Models.DTOs.OrderManagements;

namespace Common.Domain.Events.OrderManagements;

public sealed record OrderAddedEvent : IntegrationEvent
{
    public OrderAddedEvent()
    {
    }

    public OrderAddedEvent(
        Guid? correlationId,
        Guid orderId,
        int userId,
        DateTime orderDate,
        string shipmentDetail,
        List<OrderItemDto> items)
        : base(correlationId, orderDate)
    {
        OrderId = orderId;
        UserId = userId;
        OrderDate = orderDate;
        ShipmentDetail = shipmentDetail;
        Items = items;
    }

    public Guid OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShipmentDetail { get; set; }
    public List<OrderItemDto> Items { get; set; }
}