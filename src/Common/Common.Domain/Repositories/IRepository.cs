using Common.Domain.Models;

namespace Common.Domain.Repositories;

public interface IRepository<TEntity>
where TEntity : IAggregateRoot
{
    Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
}