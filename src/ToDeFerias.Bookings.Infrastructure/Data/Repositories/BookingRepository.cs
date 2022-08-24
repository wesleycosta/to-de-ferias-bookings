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

    public async override Task<Booking> GetById(Guid id) =>
        await DbSet.Include(p => p.HouseGuest)
                   .Include(p => p.Room)
                       .ThenInclude(p => p.Type)
                   .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<bool> ItsBooked(Guid roomId, DateRangeBooking dateRangeBooking) =>
        await DbSet.AnyAsync(p => p.RoomId.Equals(roomId) &&
                                  p.Period.CheckIn >= dateRangeBooking.CheckIn &&
                                  p.Period.CheckOut <= dateRangeBooking.CheckOut &&
                                 p.Status != BookingStatus.Cancelled);

    public async Task<bool> ItsBooked(Guid bookingId, Guid roomId, DateTimeOffset checkIn, DateTimeOffset checkOut) =>
        await DbSet.AnyAsync(p => p.Id != bookingId &&
                                  p.RoomId.Equals(roomId) &&
                                  p.Period.CheckIn >= checkIn &&
                                  p.Period.CheckOut <= checkOut &&
                                  p.Status != BookingStatus.Cancelled);
    public async Task<Room> GetRoomById(Guid id) =>
        await Context.Set<Room>()
                     .Include(p => p.Type)
                     .FirstOrDefaultAsync(p => p.Id.Equals(id));

    public async Task<IReadOnlyList<Booking>> GetDateRange(DateTimeOffset start, DateTimeOffset end) =>
              await DbSet.Include(p => p.HouseGuest)
                   .Include(p => p.Room)
                       .ThenInclude(p => p.Type)
                   .Where(p => p.Period.CheckIn >= start &&
                               p.Period.CheckIn <= end &&
                               p.Status != BookingStatus.Cancelled)
                   .ToListAsync();
    
    public async Task<IReadOnlyList<Booking>> GetByRoomId(Guid roomId, DateTimeOffset start, DateTimeOffset end) =>
        await DbSet.Include(p => p.HouseGuest)
                   .Include(p => p.Room)
                       .ThenInclude(p => p.Type)
                   .Where(p => p.RoomId == roomId &&
                               p.Period.CheckIn >= start &&
                               p.Period.CheckIn <= end &&
                               p.Status != BookingStatus.Cancelled)
                   .ToListAsync();
}
