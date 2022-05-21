using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Events.Bookings;

public sealed class RegisteredBookingEvent : Event
{
    public string HouseGuestName { get; private set; }
    public Email HouseGuestEmail { get; private set; }
    public byte RoomNumber { get; private set; }
    public string RoomTypeName { get; private set; }
    public DateRangeBooking DateRangeBooking { get; private set; }
    public decimal Value { get; private set; }
    public byte Adults { get; private set; }
    public byte Children { get; private set; }

    public RegisteredBookingEvent(HouseGuest houseGuest,
                                  Room room,
                                  DateRangeBooking dateRangeBooking,
                                  decimal value,
                                  byte adults,
                                  byte children)
    {
        HouseGuestName = houseGuest.Name;
        RoomNumber = room.Number;
        RoomTypeName = room?.Type?.Name;
        DateRangeBooking = dateRangeBooking;
        Value = value;
        Adults = adults;
        Children = children;
    }
}
