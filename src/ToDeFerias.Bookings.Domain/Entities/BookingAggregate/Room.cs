using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

public sealed class Room : Entity
{
    public byte Number { get; private set; }
    public RoomType Type { get; private set; }

    public Guid RoomTypeId { get; private set; }
    public List<Booking> Bookings { get; set; }

    protected Room() { }

    public Room(Guid id) =>
        Id = id;

    public Room(Guid id, byte number, Guid roomTypeId)
    {
        Id = id;
        Number = number;
        RoomTypeId = roomTypeId;
    }
}
