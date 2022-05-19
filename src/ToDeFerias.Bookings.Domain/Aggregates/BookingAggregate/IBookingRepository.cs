using ToDeFerias.Bookings.Core.Data;

namespace ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

public interface IBookingRepository : IRepositoryBase<Booking>
{
    Task<bool> ItsBooked(Guid roomId, DateRangeBooking dateRangeBooking);
    Task<Room> GetRoomById(Guid id);
}
