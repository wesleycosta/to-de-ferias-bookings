using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Repositories;

public interface IBookingRepository : IRepositoryBase<Booking>
{
    Task<bool> ItsBooked(Guid roomId, DateRangeBooking dateRangeBooking);
    Task<Room> GetRoomById(Guid id);
}
