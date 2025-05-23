using System.Reflection;
using Common.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Infrastructure.Persistence;

public class OrderDbContext : BaseDbContext<OrderDbContext>
{
    public OrderDbContext(
    DbContextOptions<OrderDbContext> options,
    IPublisher publisher)
        : base(options, publisher)
    {
    }

    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.HasDefaultSchema("order");

        base.OnModelCreating(builder);
    }
}