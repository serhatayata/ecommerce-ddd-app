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
            var payment1 = Payment.Create(OrderId.From(Guid.NewGuid()), 100.00m, PaymentMethod.CreditCard);
            payment1.RaisePaymentCompletedEvent();
            payment1.Transactions.Add(PaymentTransaction.Create(100.00m, DateTime.UtcNow, "TXN1001", PaymentStatus.Completed));

            var payment2 = Payment.Create(OrderId.From(Guid.NewGuid()), 250.50m, PaymentMethod.PayPal);
            payment2.RaisePaymentCompletedEvent();
            payment2.Transactions.Add(PaymentTransaction.Create(250.50m, DateTime.UtcNow, "TXN1002", PaymentStatus.Failed));

            var payment3 = Payment.Create(OrderId.From(Guid.NewGuid()), 75.25m, PaymentMethod.BankTransfer);
            
            payment3.Transactions.Add(PaymentTransaction.Create(75.25m, DateTime.UtcNow, "TXN1003", PaymentStatus.Completed));

            _dbContext.Payments.AddRange(payment1, payment2, payment3);
            _dbContext.SaveChanges();
        }
    }
}