using Common.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Persistence;

public class OrderManagementSagaDbInitializer : DbInitializer
{
    public OrderManagementSagaDbInitializer(OrderManagementSagaDbContext dbContext)
        : base(dbContext)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        // No specific initialization logic for Order Management Saga at this time
        // This can be extended in the future if needed
    }
}