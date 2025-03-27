using System.Linq.Expressions;
using Common.Domain.Models;
using Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repositories;

public abstract class EfRepository<T, TDbContext> : IRepository<T>
where T : Entity, IAggregateRoot
where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public EfRepository(
    TDbContext dbContext)
        => _dbContext = dbContext;

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().SingleOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression = null, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (expression != null)
            query = query.Where(expression);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task SaveAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}