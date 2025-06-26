using Common.Infrastructure.Persistence;

namespace Identity.Infrastructure.Persistence;

internal class IdentitySagaDbInitializer : DbInitializer
{
    public IdentitySagaDbInitializer(IdentitySagaDbContext dbContext)
        : base(dbContext)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        // No specific initialization logic for Identity Saga at this time
        // This can be extended in the future if needed
    }
}