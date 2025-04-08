using System.Reflection;
using Common.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models.Stocks;

namespace Stock.Infrastructure.Persistence;

public class StockDbContext : BaseDbContext<StockDbContext>
{
    public StockDbContext(
        DbContextOptions<StockDbContext> options,
        IMediator mediator)
        : base(options, mediator)
    {
    }

    public DbSet<StockItem> StockItems { get; set; }
    public DbSet<StockTransaction> StockTransactions { get; set; }
    public DbSet<StockReservation> StockReservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema("stock");

        base.OnModelCreating(modelBuilder);
    }
}