using System.Linq.Expressions;
using Common.Domain.Models;

namespace Common.Domain.Repositories;

public interface IRepository<T, R>
where T : Entity<R>, IAggregateRoot
{
    Task<int> SaveAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(R id, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}