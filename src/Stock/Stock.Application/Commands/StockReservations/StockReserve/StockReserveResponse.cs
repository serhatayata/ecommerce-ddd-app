namespace Stock.Application.Commands.StockReservations.StockReserve;

public sealed record StockReserveResponse
{
    public StockReserveResponse(
        Guid orderId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; }
}