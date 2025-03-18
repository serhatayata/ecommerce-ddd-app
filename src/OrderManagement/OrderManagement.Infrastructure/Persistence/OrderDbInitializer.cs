using Common.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Persistence;

public class OrderDbInitializer : DbInitializer
{
    public OrderDbInitializer(
        OrderDbContext db)
        : base(db)
    {
    }

    public override void Initialize()
    {
        // base.Initialize();
    }
}