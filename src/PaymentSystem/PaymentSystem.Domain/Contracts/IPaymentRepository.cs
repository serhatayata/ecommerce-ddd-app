using Common.Domain.Repositories;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Domain.Contracts;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsByOrderIdAsync(int orderId, CancellationToken cancellationToken);
    Task<List<PaymentTransaction>> GetTransactionsByPaymentIdAsync(int paymentId, CancellationToken cancellationToken);

    #region PaymentInfo
    Task<PaymentInfo> GetPaymentInfoByOrderIdAsync(int orderId, CancellationToken cancellationToken);
    Task<int> CreatePaymentInfoAsync(PaymentInfo paymentInfo, CancellationToken cancellationToken);
    #endregion
}