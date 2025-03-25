using Identity.Infrastructure.Persistence.Configurations;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;

public class IdentitySagaDbContext : SagaDbContext
{
    public IdentitySagaDbContext(
    DbContextOptions<IdentitySagaDbContext> options) : base(options)
    {
        
    }

    protected override IEnumerable<ISagaClassMap> Configurations 
    { 
        get { yield return new UserRegistrationStateMap(); }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identitysaga");

        base.OnModelCreating(modelBuilder);
    }
}