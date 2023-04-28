using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class CheckInCommand : Command
{
    public CheckInCommand(Guid aggregateId) =>
        AggregateId = aggregateId;
}
