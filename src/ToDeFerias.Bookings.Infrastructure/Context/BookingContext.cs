using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Infrastructure.Extensions;

namespace ToDeFerias.Bookings.Infrastructure.Context;

public sealed class BookingContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public BookingContext(DbContextOptions<BookingContext> options,
                                    IMediatorHandler mediatorHandler) : base(options)
    {
        _mediatorHandler = mediatorHandler;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
        return await SaveChangesAsync() > 0;
    }
}
