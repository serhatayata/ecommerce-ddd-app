using Common.Domain.Models;
using Common.Domain.ValueObjects;

namespace Stock.Domain.Models.Stocks;

/// <summary>
/// Represents a transaction that affects stock levels of an inventory item.
/// Used to track all stock movements (additions or removals) and maintain
/// an audit trail of inventory changes.
/// </summary>
public class StockTransaction : Entity
{
    private StockTransaction() { }

    public StockTransaction(
        StockItemId stockItemId,
        int quantity,
        StockTransactionType type,
        string reason)
    {
        StockItemId = stockItemId;
        Quantity = quantity;
        Type = type;
        Reason = reason;
        TransactionDate = DateTime.UtcNow;
    }

    public StockItemId StockItemId { get; private set; }
    public int Quantity { get; private set; }
    public StockTransactionType Type { get; private set; }
    public string Reason { get; private set; }
    public DateTime TransactionDate { get; private set; }

    public virtual StockItem StockItem { get; private set; }
}

/// <summary>
/// Defines the type of stock transaction being performed.
/// </summary>
public enum StockTransactionType
{
    /// <summary>
    /// Represents stock being added to inventory (e.g., new deliveries, returns)
    /// </summary>
    Addition,
    
    /// <summary>
    /// Represents stock being removed from inventory (e.g., sales, damages)
    /// </summary>
    Removal
}
