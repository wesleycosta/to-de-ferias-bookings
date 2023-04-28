using ToDeFerias.Bookings.Core.Data;

namespace ToDeFerias.Bookings.Domain.Bookings.Aggregates;

public interface IBookingRepository : IRepositoryBase<Booking>
{
    Task<bool> ItsBooked(Guid roomId, DateRangeBooking dateRangeBooking);
    Task<bool> ItsBooked(Guid bookingId, Guid roomId, DateTimeOffset checkIn, DateTimeOffset checkOut);
    Task<Room> GetRoomById(Guid id);
    Task<IReadOnlyList<Booking>> GetDateRange(DateTimeOffset start, DateTimeOffset end);
    Task<IReadOnlyList<Booking>> GetByRoomId(Guid roomId, DateTimeOffset start, DateTimeOffset end);
}
