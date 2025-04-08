using Common.Domain.Repositories;
using Stock.Domain.Models.Stocks;

namespace Stock.Domain.Contracts;

public interface IStockRepository : IRepository<StockItem>
{
    #region StockReservation
    Task<StockReservation> AddReservationAsync(StockReservation reservation);
    Task DeleteReservationAsync(StockReservation reservation);
    Task UpdateReservationAsync(StockReservation reservation);
    Task<IEnumerable<StockReservation>> GetReservationsByStockItemIdAsync(int stockItemId);
    Task<IEnumerable<StockReservation>> GetReservationsByOrderIdAsync(int orderId);
    #endregion
    
    #region StockTransaction
    Task<StockTransaction> AddTransactionAsync(StockTransaction transaction);
    Task UpdateTransactionAsync(StockTransaction transaction);
    Task DeleteTransactionAsync(StockTransaction transaction);
    Task<IEnumerable<StockTransaction>> GetTransactionsByStockItemIdAsync(int stockItemId);
    #endregion
}