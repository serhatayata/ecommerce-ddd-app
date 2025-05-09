using Common.Infrastructure.Repositories;
using OrderManagement.Domain.Contracts;
using OrderManagement.Domain.Models.Orders;
using OrderManagement.Infrastructure.Persistence;

namespace OrderManagement.Infrastructure.Repositories;

public class OrderRepository : EfRepository<Order, OrderDbContext>, IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    : base(dbContext)
    {
        _dbContext = dbContext;
    }
}