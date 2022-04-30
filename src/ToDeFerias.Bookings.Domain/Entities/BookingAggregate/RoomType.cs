using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

public sealed class RoomType : Entity
{
    public string Name { get; private set; }
    public List<Room> Rooms { get; }

    public RoomType() { }

    public RoomType(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
