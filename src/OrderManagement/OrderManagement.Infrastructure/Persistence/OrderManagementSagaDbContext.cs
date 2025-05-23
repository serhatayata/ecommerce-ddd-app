using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Infrastructure.Persistence.EntityConfigurations;

namespace OrderManagement.Infrastructure.Persistence;

public class OrderManagementSagaDbContext : SagaDbContext
{
    public OrderManagementSagaDbContext(
    DbContextOptions<OrderManagementSagaDbContext> options) : base(options)
    {
        
    }

    protected override IEnumerable<ISagaClassMap> Configurations 
    { 
        get { yield return new OrderAddStateMap(); }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ordermanagementsaga");

        base.OnModelCreating(modelBuilder);
    }
}
