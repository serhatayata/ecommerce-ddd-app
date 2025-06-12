using Common.Domain.ValueObjects;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.Contracts;
using Stock.Domain.Models.Stocks;
using Stock.Infrastructure.Persistence;

namespace Stock.Infrastructure.Repositories.StockItems;

public class StockItemRepository : EfRepository<StockItem, StockDbContext, int>, IStockItemRepository
{
    private readonly StockDbContext _dbContext;

    public StockItemRepository(StockDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    #region Stock Reservation
    public async Task<StockReservation> CreateReservationAsync(
        StockReservation reservation,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.StockReservations.AddAsync(reservation, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return reservation;
    }

    public async Task UpdateReservationAsync(
        StockReservation reservation,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(reservation).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteReservationAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var reservation = await GetReservationByIdAsync(id, cancellationToken);
        if (reservation == null) return;

        _dbContext.StockReservations.Remove(reservation);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<StockReservation> GetReservationByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockReservations.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<IEnumerable<StockReservation>> GetReservationsByOrderIdAsync(
        OrderId orderId,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockReservations
            .Where(s => s.OrderId == orderId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<StockReservation>> GetReservationsByStockItemIdAsync(
        StockItemId stockItemId,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockReservations
            .Where(s => s.StockItemId == stockItemId.Value)
            .ToListAsync(cancellationToken);

    public async Task<List<(int StockItemId, int Quantity)>> ReserveProductsStocks(
    Dictionary<ProductId, int> productQuantities,
    OrderId orderId, 
    CancellationToken cancellationToken = default)
    {
        var productIds = productQuantities.Keys.ToList();
        var stockItems = await GetByProductIdsAsync(productIds, cancellationToken);

        var reservedItems = new List<(int StockItemId, int Quantity)>();
        foreach (var productQuantity in productQuantities)
        {
            var productId = productQuantity.Key;
            var quantity = productQuantity.Value;
            var stockItem = stockItems.FirstOrDefault(x => x.ProductId == productId);
            if (stockItem != null)
            {
                await ReserveStock(reservedItems, stockItem, quantity, orderId, cancellationToken);
            }
            else
            {
                var newStockItem = StockItem.Create(
                    productId,
                    quantity,
                    new Location(string.Empty, string.Empty, string.Empty)
                );

                _ = await SaveAsync(newStockItem, cancellationToken);
                await ReserveStock(reservedItems, newStockItem, quantity, orderId, cancellationToken);
            }
        }

        return reservedItems;
    }

    private async Task<List<(int StockItemId, int Quantity)>> ReserveStock(
    List<(int StockItemId, int Quantity)> reservedItems,
    StockItem stockItem,
    int quantity,
    OrderId orderId,
    CancellationToken cancellationToken)
    {
        stockItem.ReserveStock(quantity, orderId);
        var isUpdated = await UpdateAsync(stockItem, cancellationToken) > 0;
        if (isUpdated)
            reservedItems.Add((stockItem.Id, quantity));
            
        return reservedItems;
    }
    #endregion

    #region Stock Transaction
    public async Task<StockTransaction> CreateTransactionAsync(
        StockTransaction transaction,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.StockTransactions.AddAsync(transaction, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return transaction;
    }

    public async Task UpdateTransactionAsync(
        StockTransaction transaction,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(transaction).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTransactionAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var transaction = await GetTransactionByIdAsync(id, cancellationToken);
        if (transaction == null) return;

        _dbContext.StockTransactions.Remove(transaction);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<StockTransaction>> GetTransactionsByStockItemIdAsync(
        StockItemId stockItemId,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockTransactions
            .Where(s => s.StockItemId == stockItemId.Value)
            .ToListAsync(cancellationToken);

    public async Task<StockTransaction> GetTransactionByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockTransactions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    #endregion

    public async Task<StockItem> GetByIdWithReservationsAsync(StockItemId id, CancellationToken cancellationToken)
        => await _dbContext.StockItems
            .Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

    public async Task<IEnumerable<StockItem>> GetByProductIdsAsync(
    IEnumerable<ProductId> productIds, 
    CancellationToken cancellationToken = default)
    {
        return await _dbContext.StockItems
            .Include(x => x.Reservations)
            .Where(x => productIds.Contains(x.ProductId))
            .ToListAsync(cancellationToken);
    }
}