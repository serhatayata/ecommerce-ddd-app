using Common.Domain.Repositories;
using Common.Domain.ValueObjects;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Domain.Contracts;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(PaymentId id, CancellationToken cancellationToken);
    Task<bool> ExistsByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken);
    Task<List<PaymentTransaction>> GetTransactionsByPaymentIdAsync(PaymentId paymentId, CancellationToken cancellationToken);

    #region PaymentInfo
    Task<PaymentInfo> GetPaymentInfoByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken);
    Task<int> CreatePaymentInfoAsync(PaymentInfo paymentInfo, CancellationToken cancellationToken);
    #endregion
}