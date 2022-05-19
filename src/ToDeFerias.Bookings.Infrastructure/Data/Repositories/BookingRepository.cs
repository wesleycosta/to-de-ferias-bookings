using Microsoft.EntityFrameworkCore;
using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Infrastructure.Context;
using ToDeFerias.Bookings.Infrastructure.Repositories;

namespace ToDeFerias.Bookings.Infrastructure.Data.Repositories;

internal sealed class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(BookingContext context,
                             IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Room> GetRoomById(Guid id) =>
        await Context.Set<Room>()
                     .FirstOrDefaultAsync(p => p.Id.Equals(id));

    public async Task<bool> ItsBooked(Guid roomId, DateRangeBooking dateRangeBooking) =>
        await DbSet.AnyAsync(p => p.RoomId.Equals(roomId) &&
                             p.DateRange.CheckIn >= dateRangeBooking.CheckIn &&
                             p.DateRange.CheckOut <= dateRangeBooking.CheckOut);
}
