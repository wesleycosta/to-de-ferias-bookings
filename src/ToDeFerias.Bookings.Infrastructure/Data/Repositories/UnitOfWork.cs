using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Infrastructure.Context;

namespace ToDeFerias.Bookings.Infrastructure.Data.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly BookingContext _context;

    public UnitOfWork(BookingContext bookingContext) =>
        _context = bookingContext;

    public async Task<bool> Commit() =>
        await _context.Commit();
}
