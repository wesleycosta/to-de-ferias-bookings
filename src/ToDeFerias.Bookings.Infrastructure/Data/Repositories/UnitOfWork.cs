using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Infrastructure.Context;

namespace ToDeFerias.Bookings.Infrastructure.Data.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly BookingsContext _context;

    public UnitOfWork(BookingsContext bookingsContext) =>
        _context = bookingsContext;

    public async Task<bool> Commit() =>
        await _context.Commit();
}
