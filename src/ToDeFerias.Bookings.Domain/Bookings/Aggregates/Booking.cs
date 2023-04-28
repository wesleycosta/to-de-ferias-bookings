using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;

namespace ToDeFerias.Bookings.Domain.Bookings.Aggregates;

public sealed class Booking : Entity, IAggregateRoot
{
    public HouseGuest HouseGuest { get; private set; }
    public Room Room { get; private set; }
    public DateRangeBooking Period { get; private set; }
    public decimal Value { get; private set; }
    public byte Adults { get; private set; }
    public byte Children { get; private set; }
    public BookingStatus Status { get; private set; }

    public Guid HouseGuestId { get; private set; }
    public Guid RoomId { get; private set; }

    public Booking() { }

    public Booking(Guid houseGuestId,
        Guid roomId,
        DateTimeOffset checkIn,
        DateTimeOffset checkOut,
        decimal value,
        byte adults,
        byte children)
    {
        Id = Guid.NewGuid();
        HouseGuestId = houseGuestId;
        RoomId = roomId;
        Period = new(checkIn, checkOut);
        Value = value;
        Adults = adults;
        Children = children;
        Status = BookingStatus.Booked;
    }

    public void Update(decimal value,
        byte adults,
        byte children,
        DateTimeOffset checkIn,
        DateTimeOffset checkOut)
    {
        Value = value;
        Adults = adults;
        Children = children;
        Period = new(checkIn, checkOut);
    }

    public bool HasDateChanged(DateRangeBooking period) =>
        !Period.Equals(period);

    public bool IsItPossibleToCancel() =>
        Status == BookingStatus.Booked;

    public bool IsItPossibleToCheckIn() =>
        Status == BookingStatus.Booked;

    public bool IsItPossibleToCheckOut() =>
        Status == BookingStatus.CheckIn;

    public void CheckIn()
    {
        if (!IsItPossibleToCheckIn())
            throw new DomainException("It is only possible to check in for bookings that have the same status as Booked");

        Status = BookingStatus.CheckIn;
    }

    public void CheckOut()
    {
        if (!IsItPossibleToCheckOut())
            throw new DomainException("It is only possible to check out for bookings that have the same status as CheckIn");

        Status = BookingStatus.CheckOut;
    }

    public void Cancel()
    {
        if (!IsItPossibleToCancel())
            throw new DomainException("It is only possible to cancel for bookings that have the same status as Booked");

        Status = BookingStatus.Cancelled;
    }
}
