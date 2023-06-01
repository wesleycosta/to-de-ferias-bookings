using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Commands;

public class DeleteHouseGuestCommand : Command
{
    public DeleteHouseGuestCommand(Guid aggregateId)
        => AggregateId = aggregateId;
}
