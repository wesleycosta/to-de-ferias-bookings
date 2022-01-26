using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

public sealed class Room : Entity
{
    public byte Number { get; private set; }
    public RoomType Type { get; private set; }

    public Guid RoomTypeId { get; set; }
    public List<Booking> Bookings { get; set; }

    protected Room() { }

    public Room(Guid roomId) =>
        Id = roomId;
}
