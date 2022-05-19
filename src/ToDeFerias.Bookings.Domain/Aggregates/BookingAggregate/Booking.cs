using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

public sealed class Booking : Entity, IAggregateRoot
{
    public HouseGuest HouseGuest { get; private set; }
    public Room Room { get; private set; }
    public DateRangeBooking DateRange { get; private set; }
    public decimal Value { get; private set; }
    public byte Adults { get; private set; }
    public byte Children { get; private set; }
    public BookingStatus Status { get; private set; }

    public Guid HouseGuestId { get; private set; }
    public Guid RoomId { get; private set; }

    public Booking() { }

    public Booking(Guid houseGuestId,
                   Guid roomId,
                   DateTime checkIn,
                   DateTime checkOut,
                   decimal value,
                   byte adults,
                   byte children)
    {
        Id = Guid.NewGuid();
        HouseGuestId = houseGuestId;
        RoomId = roomId;
        DateRange = new(checkIn, checkOut);
        Value = value;
        Adults = adults;
        Children = children;
        Status = BookingStatus.Booked;
    }
}
