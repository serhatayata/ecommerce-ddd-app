using System.Reflection;
using Common.Domain.ValueObjects;
using Common.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Infrastructure.Persistence;

public class PaymentSystemDbContext : BaseDbContext<PaymentSystemDbContext>
{
    public PaymentSystemDbContext(
        DbContextOptions<PaymentSystemDbContext> options,
        IPublisher publisher)
        : base(options, publisher)
    {
    }

    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; } = null!;
    public DbSet<PaymentInfo> PaymentInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<OrderId>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema("paymentsystem");

        base.OnModelCreating(modelBuilder);
    }   
}