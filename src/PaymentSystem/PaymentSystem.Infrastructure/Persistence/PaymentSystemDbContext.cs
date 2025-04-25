using System.Reflection;
using Common.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Infrastructure.Persistence;

public class PaymentSystemDbContext : BaseDbContext<PaymentSystemDbContext>
{
    public PaymentSystemDbContext(
        DbContextOptions<PaymentSystemDbContext> options,
        IMediator mediator)
        : base(options, mediator)
    {
    }

    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema("paymentsystem");

        base.OnModelCreating(modelBuilder);
    }   
}