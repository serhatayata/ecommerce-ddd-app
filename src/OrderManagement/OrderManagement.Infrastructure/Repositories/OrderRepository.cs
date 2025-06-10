using Common.Domain.ValueObjects;
using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Order> GetByIdWithItemsAsync(
    Guid id, 
    CancellationToken cancellationToken = default)
        => await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
}