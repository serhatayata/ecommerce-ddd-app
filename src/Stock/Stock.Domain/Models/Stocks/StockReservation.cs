using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace Stock.Domain.Models.Stocks;

/// <summary>
/// Represents a temporary reservation of stock items for a specific order.
/// This class manages the lifecycle of stock reservations including creation,
/// completion, and cancellation, helping to prevent overselling by holding
/// inventory during the order process.
/// </summary>
public class StockReservation : Entity
{
    private StockReservation() { }

    private StockReservation(
        int stockItemId,
        OrderId orderId,
        int quantity)
    {
        StockItemId = stockItemId;
        OrderId = orderId;
        Quantity = quantity;
        ReservationDate = DateTime.UtcNow;
        Status = ReservationStatus.Active;
    }

    public static StockReservation Create(
        int stockItemId,
        OrderId orderId,
        int quantity)
        => new StockReservation(stockItemId, orderId, quantity);

    public int StockItemId { get; private set; }
    public OrderId OrderId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime ReservationDate { get; private set; }
    public ReservationStatus Status { get; private set; }

    public virtual StockItem StockItem { get; private set; }

    public void Cancel()
    {
        Status = ReservationStatus.Cancelled;
    }

    public void Complete()
    {
        Status = ReservationStatus.Completed;
    }
}

public enum ReservationStatus
{
    Active,
    Completed,
    Cancelled
}
