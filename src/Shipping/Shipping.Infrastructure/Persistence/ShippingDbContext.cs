using System.Reflection;
using Common.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Models.Shipments;

namespace Shipping.Infrastructure.Persistence;

public class ShippingDbContext : BaseDbContext<ShippingDbContext>
{
    public ShippingDbContext(
    DbContextOptions<ShippingDbContext> options,
    IMediator mediator)
    : base(options, mediator)
    {
    }

    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShipmentItem> ShipmentItems { get; set; }
    public DbSet<ShipmentCompany> ShipmentCompanies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema("shipping");

        base.OnModelCreating(modelBuilder);
    }
}
