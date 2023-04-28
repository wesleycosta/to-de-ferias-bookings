using System.Net.Mail;
using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Domain.Bookings.Aggregates;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;

public sealed class HouseGuest : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public List<Booking> Bookings { get; set; }

    public HouseGuest() { }

    public HouseGuest(string name,
        string emailAddress,
        string cpf,
        DateTimeOffset birthday)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = new Email(emailAddress);
        Cpf = new Cpf(cpf);
        DateOfBirth = new DateOfBirth(birthday);
    }

    public void SetId(Guid id)
        => Id = id;

    public void Update(string name,
        string email,
        string cpf,
        DateTimeOffset birthday)
    {
        Name = name;
        Email = new Email(email);
        Cpf = new Cpf(cpf);
        DateOfBirth = new DateOfBirth(birthday);
    }
}
