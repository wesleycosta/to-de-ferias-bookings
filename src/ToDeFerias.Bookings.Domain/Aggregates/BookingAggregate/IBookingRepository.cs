using ToDeFerias.Bookings.Core.Data;

namespace ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

public interface IBookingRepository : IRepositoryBase<Booking>
{
    Task<bool> ItsBooked(Guid roomId, DateRangeBooking dateRangeBooking);
    Task<bool> ItsBooked(Guid bookingId, Guid roomId, DateTimeOffset checkIn, DateTimeOffset checkOut);
    Task<Room> GetRoomById(Guid id);
}
