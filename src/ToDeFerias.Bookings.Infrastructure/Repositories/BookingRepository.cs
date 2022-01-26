using Microsoft.EntityFrameworkCore;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;
using ToDeFerias.Bookings.Domain.Repositories;
using ToDeFerias.Bookings.Infrastructure.Context;

namespace ToDeFerias.Bookings.Infrastructure.Repositories;

public sealed class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(BookingContext context) : base(context)
    {
    }

    public async Task<Room?> GetRoomById(Guid id) =>
        await Context.Set<Room>()
                     .FirstOrDefaultAsync(p => p.Id.Equals(id));

    public async Task<bool> ItsBooked(Guid roomId, DateRangeBooking dateRangeBooking) =>
        await DbSet.AnyAsync(p => p.RoomId.Equals(roomId) &&
                             p.DateRange.CheckIn >= dateRangeBooking.CheckIn &&
                             p.DateRange.CheckOut <= dateRangeBooking.CheckOut);
}
