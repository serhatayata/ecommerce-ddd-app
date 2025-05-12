using System.Linq.Expressions;
using Common.Domain.Models;

namespace Common.Domain.Repositories;

public interface IRepository<T>
where T : Entity, IAggregateRoot
{
    Task<int> SaveAsync(T entity, CancellationToken cancellationToken = default);
    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}