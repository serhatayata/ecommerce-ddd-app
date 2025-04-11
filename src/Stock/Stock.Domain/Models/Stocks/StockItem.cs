using Common.Domain.Models;
using Stock.Domain.Events;

namespace Stock.Domain.Models.Stocks;

public class StockItem : Entity, IAggregateRoot
{
    private readonly List<StockTransaction> _transactions = new();
    private readonly List<StockReservation> _reservations = new();

    private StockItem() { }

    public StockItem(
        int productId,
        int quantity,
        Location location)
    {
        ProductId = productId;
        Quantity = quantity;
        Location = location;
        Status = StockStatus.Available;
        LastUpdated = DateTime.UtcNow;

        AddEvent(new StockItemCreatedDomainEvent(Id, ProductId, Quantity));
    }

    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Location Location { get; private set; }
    public StockStatus Status { get; private set; }
    public DateTime LastUpdated { get; private set; }

    public ICollection<StockTransaction> Transactions => _transactions.AsReadOnly();
    public ICollection<StockReservation> Reservations => _reservations.AsReadOnly();

    public void AddStock(int quantity, string reason)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        Quantity += quantity;
        LastUpdated = DateTime.UtcNow;

        var transaction = new StockTransaction(Id, quantity, StockTransactionType.Addition, reason);
        _transactions.Add(transaction);

        AddEvent(new StockAddedDomainEvent(Id, quantity));
    }

    public void RemoveStock(int quantity, string reason)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive", nameof(quantity));

        if (Quantity < quantity)
            throw new InvalidOperationException("Insufficient stock");

        Quantity -= quantity;
        LastUpdated = DateTime.UtcNow;

        var transaction = new StockTransaction(Id, quantity, StockTransactionType.Removal, reason);
        _transactions.Add(transaction);

        AddEvent(new StockRemovedDomainEvent(Id, quantity));
    }

    public void ReserveStock(int quantity, int orderId)
    {
        if (GetAvailableQuantity() < quantity)
            throw new InvalidOperationException("Insufficient stock for reservation");

        var reservation = new StockReservation(Id, orderId, quantity);
        _reservations.Add(reservation);

        AddEvent(new StockReservedDomainEvent(Id, quantity, orderId));
    }

    public int GetAvailableQuantity()
    {
        var reservedQuantity = _reservations.Sum(r => r.Quantity);
        return Quantity - reservedQuantity;
    }
}
