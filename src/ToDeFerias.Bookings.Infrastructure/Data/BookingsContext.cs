using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Infrastructure.Extensions;

namespace ToDeFerias.Bookings.Infrastructure.Context;

public sealed class BookingsContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public BookingsContext(DbContextOptions<BookingsContext> options,
                          IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach (var property in modelBuilder.Model
                                             .GetEntityTypes()
                                             .SelectMany(e => e.GetProperties()
                                                               .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("VARCHAR(255)");

        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
        return await SaveChangesAsync() > 0;
    }
}
