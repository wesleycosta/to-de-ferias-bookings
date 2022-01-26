using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Domain.Entities.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

public sealed class Booking : Entity, IAggregateRoot
{
    public HouseGuest HouseGuest { get; set; }
    public Room Room { get; private set; }
    public DateRangeBooking DateRange { get; private set; }
    public decimal Value { get; private set; }
    public byte Adults { get; private set; }
    public byte Children { get; private set; }
    public BookingStatus Status { get; private set; }

    public Guid HouseGuestId { get; set; }
    public Guid RoomId { get; private set; }

    protected Booking() { }

    public Booking(Guid houseGuestId,
                   Guid roomId,
                   DateTime checkIn,
                   DateTime checkOut,
                   decimal value,
                   byte adults,
                   byte children)
    {
        HouseGuest = new HouseGuest(houseGuestId);
        Room = new Room(roomId);
        DateRange = new(checkIn, checkOut);
        Value = value;
        Adults = adults;
        Children = children;
        Status = BookingStatus.Booked;
    }
}
