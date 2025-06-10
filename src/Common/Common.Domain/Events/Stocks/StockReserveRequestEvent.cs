using Common.Domain.Models.DTOs.OrderManagements;

namespace Common.Domain.Events.Stocks;

public sealed record StockReserveRequestEvent : IntegrationEvent
{
    public StockReserveRequestEvent()
    {
    }

    public StockReserveRequestEvent(
    Guid? correlationId,
    Guid orderId,
    List<OrderItemDto> items)
    : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        Items = items;
    }

    public Guid OrderId { get; set; }
    public List<OrderItemDto> Items { get; set; }
}
