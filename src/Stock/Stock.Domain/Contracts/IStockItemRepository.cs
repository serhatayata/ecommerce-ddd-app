using Common.Domain.Repositories;
using Stock.Domain.Models.Stocks;

namespace Stock.Domain.Contracts;

public interface IStockItemRepository : IRepository<StockItem>
{
    #region StockReservation
    Task<StockReservation> GetReservationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<StockReservation> CreateReservationAsync(StockReservation reservation, CancellationToken cancellationToken = default);
    Task DeleteReservationAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateReservationAsync(StockReservation reservation, CancellationToken cancellationToken = default);
    Task<IEnumerable<StockReservation>> GetReservationsByStockItemIdAsync(int stockItemId, CancellationToken cancellationToken = default);
    Task<IEnumerable<StockReservation>> GetReservationsByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
    #endregion
    
    #region StockTransaction
    Task<StockTransaction> GetTransactionByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<StockTransaction> CreateTransactionAsync(StockTransaction transaction, CancellationToken cancellationToken = default);
    Task UpdateTransactionAsync(StockTransaction transaction, CancellationToken cancellationToken = default);
    Task DeleteTransactionAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StockTransaction>> GetTransactionsByStockItemIdAsync(int stockItemId, CancellationToken cancellationToken = default);
    #endregion

    Task<StockItem> GetByIdWithReservationsAsync(int id, CancellationToken cancellationToken);
}