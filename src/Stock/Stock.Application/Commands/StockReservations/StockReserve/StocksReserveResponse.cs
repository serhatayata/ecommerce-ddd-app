namespace Stock.Application.Commands.StockReservations.StockReserve;

public sealed record StocksReserveResponse
{
    public StocksReserveResponse(
        int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
}