using Microsoft.EntityFrameworkCore;
using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Core.Mediator;

namespace ToDeFerias.Bookings.Infrastructure.Extensions;

internal static class MediatorExtension
{
    public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) => await mediator.PublishEvent(domainEvent));

        await Task.WhenAll(tasks);
    }
}
