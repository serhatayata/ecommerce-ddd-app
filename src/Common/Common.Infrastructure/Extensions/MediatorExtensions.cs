using Common.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEventsAsync<TContext>(
    this IPublisher publisher,
    TContext ctx,
    CancellationToken cancellationToken = default) where TContext : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Events)
            .ToList();

        domainEntities
            .ForEach(entity => entity.Entity.ClearEvents());

        var notifications = domainEvents.Select(domainEvent => publisher.Publish(domainEvent, cancellationToken));
        await Task.WhenAll(notifications);
    }
}