using Common.Domain.ValueObjects;
using Common.Infrastructure.Persistence;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Infrastructure.Persistence;

public class OrderDbInitializer : DbInitializer
{
    private readonly OrderDbContext _db;

    public OrderDbInitializer(
        OrderDbContext db)
        : base(db)
    {
        _db = db;
    }

    public override void Initialize()
    {
        if (!_db.Set<Order>().Any())
        {
            var order1 = Order.Create(
                userId: UserId.From(1),
                orderDate: DateTime.UtcNow.AddDays(-2)
            );
            var order2 = Order.Create(
                userId: UserId.From(2),
                orderDate: DateTime.UtcNow.AddDays(-1)
            );

            var item1 = OrderItem.Create(order1.Id, 1, 2, Money.From(10.0M));
            var item2 = OrderItem.Create(order1.Id, 2, 1, Money.From(20.0M));
            var item3 = OrderItem.Create(order2.Id, 3, 5, Money.From(5.0M));

            order1.OrderItems.Add(item1);
            order1.OrderItems.Add(item2);
            order2.OrderItems.Add(item3);

            _db.Set<Order>().Add(order1);
            _db.Set<Order>().Add(order2);

            _db.SaveChanges();
        }
    }
}