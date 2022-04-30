using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Entities.HouseGuestAggregate;

public sealed class HouseGuest : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public List<Booking> Bookings { get; set; }

    public HouseGuest() { }

    public HouseGuest(Guid id) =>
        Id = id;

    public HouseGuest(Guid id, string name, string emailAddress)
    {
        Id = id;
        Name = name;
        Email = new Email(emailAddress);
    }
}
