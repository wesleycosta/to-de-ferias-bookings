using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

public sealed class Room : Entity
{
    public byte Number { get; private set; }
    public RoomType Type { get; private set; }

    public Guid RoomTypeId { get; private set; }
    public List<Booking> Bookings { get; private set; }

    public Room() { }

    public Room(Guid id) =>
        Id = id;

    public Room(Guid id, 
        byte number, 
        Guid roomTypeId)
    {
        Id = id;
        Number = number;
        RoomTypeId = roomTypeId;
    }

    public void SetId(Guid id) =>
        Id = id;

    public void SetType(RoomType type) =>
        Type = type;
}
