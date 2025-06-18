using Common.Domain.Repositories;
using Common.Domain.ValueObjects;
using Stock.Domain.Models.Stocks;

namespace Stock.Domain.Contracts;

public interface IStockItemRepository : IRepository<StockItem, int>
{
    #region StockReservation
    Task<StockReservation> GetReservationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StockReservation>> GetReservationsByStockItemIdAsync(StockItemId stockItemId, CancellationToken cancellationToken = default);
    Task<IEnumerable<StockReservation>> GetReservationsByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);
    Task<List<(int StockItemId, int Quantity)>> ReserveProductsStocks(Dictionary<ProductId, int> productQuantities, OrderId orderId, CancellationToken cancellationToken = default);
    #endregion

    #region StockTransaction
    Task<StockTransaction> GetTransactionByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<StockTransaction>> GetTransactionsByStockItemIdAsync(StockItemId stockItemId, CancellationToken cancellationToken = default);
    #endregion

    Task<StockItem> GetByIdWithReservationsAsync(StockItemId id, CancellationToken cancellationToken);
    Task<IEnumerable<StockItem>> GetByProductIdsAsync(IEnumerable<ProductId> productIds, CancellationToken cancellationToken = default);
}