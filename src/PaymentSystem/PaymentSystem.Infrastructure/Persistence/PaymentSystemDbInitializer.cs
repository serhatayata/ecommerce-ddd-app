using Common.Domain.ValueObjects;
using Common.Infrastructure.Persistence;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Infrastructure.Persistence;

public class PaymentSystemDbInitializer : DbInitializer
{
    PaymentSystemDbContext _dbContext;

    public PaymentSystemDbInitializer(PaymentSystemDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Initialize()
    {
        base.Initialize();

        if (!_dbContext.Payments.Any())
        {
            var payment1 = new Payment(1, 100.00m, PaymentMethod.CreditCard);
            payment1.MarkAsCompleted();
            payment1.Transactions.Add(new PaymentTransaction(100.00m, DateTime.UtcNow, "TXN1001", PaymentStatus.Completed));

            var payment2 = new Payment(2, 250.50m, PaymentMethod.PayPal);
            payment2.MarkAsFailed();
            payment2.Transactions.Add(new PaymentTransaction(250.50m, DateTime.UtcNow, "TXN1002", PaymentStatus.Failed));

            var payment3 = new Payment(3, 75.25m, PaymentMethod.BankTransfer);
            
            payment3.Transactions.Add(new PaymentTransaction(75.25m, DateTime.UtcNow, "TXN1003", PaymentStatus.Completed));

            _dbContext.Payments.AddRange(payment1, payment2, payment3);
            _dbContext.SaveChanges();
        }
    }
}