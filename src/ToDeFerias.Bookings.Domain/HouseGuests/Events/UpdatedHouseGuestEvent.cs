using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Events;

public class UpdatedHouseGuestEvent : Event
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public UpdatedHouseGuestEvent(Guid aggregateId,
        string name,
        string email)
    {
        AggregateId = aggregateId;
        Name = name;
        Email = email;
    }
}
