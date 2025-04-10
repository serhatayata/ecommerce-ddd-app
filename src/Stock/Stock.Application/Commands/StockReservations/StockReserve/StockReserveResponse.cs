namespace Stock.Application.Commands.StockReservations.StockReserve;

public sealed record StockReserveResponse
{
    public StockReserveResponse(
        int stockItemId, 
        int reservedQuantity, 
        int orderId)
    {
        StockItemId = stockItemId;
        ReservedQuantity = reservedQuantity;
        OrderId = orderId;
    }

    public int StockItemId { get; }
    public int ReservedQuantity { get; }
    public int OrderId { get; }
}