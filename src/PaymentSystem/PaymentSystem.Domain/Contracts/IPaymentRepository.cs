using PaymentSystem.Domain.Models;

namespace PaymentSystem.Domain.Contracts;

public interface IPaymentRepository
{
    Task<Payment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Payment> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
    Task AddAsync(Payment payment, CancellationToken cancellationToken);
    Task UpdateAsync(Payment payment, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
}