using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Events.Bookings;

public sealed class RegisteredBookingEvent : Event
{
    public string HouseGuestName { get; private set; }
    public string HouseGuestEmail { get; private set; }
    public byte RoomNumber { get; private set; }
    public string RoomTypeName { get; private set; }
    public DateTimeOffset CheckIn { get; private set; }
    public DateTimeOffset CheckOut { get; private set; }
    public decimal Value { get; private set; }
    public byte Adults { get; private set; }
    public byte Children { get; private set; }

    public RegisteredBookingEvent(Guid aggregateId,
        HouseGuest houseGuest,
        Room room,
        DateRangeBooking dateRangeBooking,
        decimal value,
        byte adults,
        byte children)
    {
        AggregateId = aggregateId;
        HouseGuestName = houseGuest.Name;
        HouseGuestEmail = houseGuest.Email.Address;

        RoomNumber = room.Number;
        RoomTypeName = room?.Type?.Name;
        CheckIn = dateRangeBooking.CheckIn;
        CheckOut = dateRangeBooking.CheckOut;
        Value = value;
        Adults = adults;
        Children = children;
    }
}
