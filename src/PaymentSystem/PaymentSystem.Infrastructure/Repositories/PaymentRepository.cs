using Common.Domain.ValueObjects;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Domain.Contracts;
using PaymentSystem.Domain.Models;
using PaymentSystem.Infrastructure.Persistence;

namespace PaymentSystem.Infrastructure.Repositories;

public class PaymentRepository : EfRepository<Payment, PaymentSystemDbContext>, IPaymentRepository
{
    private readonly PaymentSystemDbContext _dbContext;

    public PaymentRepository(PaymentSystemDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsAsync(
    PaymentId id,
    CancellationToken cancellationToken)
        => await _dbContext.Payments.AnyAsync(s => s.Id == id.Value, cancellationToken);

    public async Task<bool> ExistsByOrderIdAsync(
    OrderId orderId,
    CancellationToken cancellationToken)
        => await _dbContext.Payments.AnyAsync(s => s.OrderId == orderId, cancellationToken);

    public async Task<Payment> GetByOrderIdAsync(
    OrderId orderId,
    CancellationToken cancellationToken)
        => await _dbContext.Payments.FirstOrDefaultAsync(s => s.OrderId == orderId, cancellationToken);

    public async Task<List<PaymentTransaction>> GetTransactionsByPaymentIdAsync(
    PaymentId paymentId,
    CancellationToken cancellationToken)
        => await _dbContext.PaymentTransactions
            .Where(s => s.PaymentId == paymentId)
            .ToListAsync(cancellationToken);

    #region PaymentInfo
    public async Task<PaymentInfo> GetPaymentInfoByOrderIdAsync(
    OrderId orderId,
    CancellationToken cancellationToken)
        => await _dbContext.PaymentInfo
            .FirstOrDefaultAsync(s => s.OrderId == orderId, cancellationToken);

    public async Task<int> CreatePaymentInfoAsync(
    PaymentInfo paymentInfo,
    CancellationToken cancellationToken)
    {
        await _dbContext.PaymentInfo.AddAsync(paymentInfo, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return paymentInfo.Id;
    }
    #endregion
}