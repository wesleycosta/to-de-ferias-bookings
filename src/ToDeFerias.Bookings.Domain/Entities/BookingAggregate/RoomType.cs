using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

public sealed class RoomType : Entity
{
    public string? Name { get; set; }
    public List<Room> Rooms { get; set; }
}
