using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.Contracts;
using Stock.Domain.Models.Stocks;
using Stock.Infrastructure.Persistence;

namespace Stock.Infrastructure.Repositories.StockItems;

public class StockItemRepository : EfRepository<StockItem, StockDbContext>, IStockItemRepository
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
        int orderId,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockReservations
            .Where(s => s.OrderId == orderId)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<StockReservation>> GetReservationsByStockItemIdAsync(
        int stockItemId,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockReservations
            .Where(s => s.StockItemId == stockItemId)
            .ToListAsync(cancellationToken);
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
        int stockItemId,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockTransactions
            .Where(s => s.StockItemId == stockItemId)
            .ToListAsync(cancellationToken);

    public async Task<StockTransaction> GetTransactionByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
        => await _dbContext.StockTransactions.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    #endregion

    public async Task<StockItem> GetByIdWithReservationsAsync(int id, CancellationToken cancellationToken)
        => await _dbContext.StockItems
            .Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
}