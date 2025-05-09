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
    int id, 
    CancellationToken cancellationToken)
        => await _dbContext.Payments.AnyAsync(s => s.Id == id, cancellationToken);

    public async Task<bool> ExistsByOrderIdAsync(
    int orderId, 
    CancellationToken cancellationToken)
        => await _dbContext.Payments.AnyAsync(s => s.OrderId == orderId, cancellationToken);

    public async Task<Payment> GetByOrderIdAsync(
    int orderId, 
    CancellationToken cancellationToken)
        => await _dbContext.Payments.FirstOrDefaultAsync(s => s.OrderId == orderId, cancellationToken);

    public async Task<List<PaymentTransaction>> GetTransactionsByPaymentIdAsync(
    int paymentId, 
    CancellationToken cancellationToken)
        => await _dbContext.PaymentTransactions
            .Where(s => s.PaymentId == paymentId)
            .ToListAsync(cancellationToken);
}