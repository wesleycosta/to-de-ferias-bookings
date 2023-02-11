using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Events.Bookings;

public sealed class BookingDateChangedEvent : Event
{
    public string HouseGuestName { get; private set; }
    public Email HouseGuestEmail { get; private set; }
    public byte RoomNumber { get; private set; }
    public string RoomTypeName { get; private set; }
    public DateRangeBooking OldDate { get; private set; }
    public DateRangeBooking NewDate { get; private set; }

    public BookingDateChangedEvent(HouseGuest houseGuest,
        Room room,
        DateRangeBooking oldDate,
        DateRangeBooking newDate)
    {
        HouseGuestName = houseGuest.Name;
        RoomNumber = room.Number;
        RoomTypeName = room?.Type?.Name;
        OldDate = oldDate;
        NewDate = newDate;
    }
}
