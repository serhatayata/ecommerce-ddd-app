using Common.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.DbContexts;

public abstract class BaseDbContext<TContext> : DbContext where TContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly Stack<object> _savesChangesTracker;

    protected BaseDbContext(DbContextOptions<TContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
        _savesChangesTracker = new Stack<object>();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _savesChangesTracker.Push(new object());

        var entitiesWithEvents = ChangeTracker
            .Entries<IEntity>()
            .Where(e => e.Entity.Events.Any())
            .Select(e => e.Entity)
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.Events.ToArray();
            entity.ClearEvents();

            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent);
            }
        }

        _savesChangesTracker.Pop();

        if (!_savesChangesTracker.Any())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        return 0;
    }
}