using Common.Domain.Repositories;
using OrderManagement.Domain.Models.Orders;

namespace OrderManagement.Domain.Contracts;

public interface IOrderRepository : IRepository<Order, Guid>
{
    public Task<Order> GetByIdWithItemsAsync(Guid id, CancellationToken cancellationToken = default);
}