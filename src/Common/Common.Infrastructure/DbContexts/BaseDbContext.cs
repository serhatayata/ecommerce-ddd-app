using Common.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.DbContexts;

public abstract class BaseDbContext<TContext> : DbContext where TContext : DbContext
{
    private readonly IPublisher _publisher;

    protected BaseDbContext(DbContextOptions<TContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var changes = await base.SaveChangesAsync(cancellationToken);

        await _publisher.DispatchDomainEventsAsync(this, cancellationToken);

        return changes;
    }
}