using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Events;

public class RegisteredHouseguestEvent : Event
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public RegisteredHouseguestEvent(Guid aggregateId,
        string name,
        string email)
    {
        AggregateId = aggregateId;
        Name = name;
        Email = email;
    }
}
