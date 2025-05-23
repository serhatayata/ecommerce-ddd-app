namespace Stock.Application.Commands.StockReservations.StockReserve;

public sealed record StockReserveResponse
{
    public StockReserveResponse(
        int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
}