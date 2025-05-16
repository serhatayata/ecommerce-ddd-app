using Common.Domain.Models.DTOs.OrderManagements;

namespace Common.Domain.Events.Stocks;

public sealed record StockReserveRequestEvent : IntegrationEvent
{
    public int OrderId { get; }
    public List<OrderItemDto> Items { get; set; }

    public StockReserveRequestEvent(
    Guid? correlationId,
    int orderId,
    List<OrderItemDto> items)
    : base(correlationId, DateTime.UtcNow)
    {
        OrderId = orderId;
        Items = items;
    }
}
