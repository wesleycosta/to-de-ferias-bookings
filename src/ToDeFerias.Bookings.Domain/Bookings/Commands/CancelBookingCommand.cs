using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class CancelBookingCommand : Command
{
    public CancelBookingCommand(Guid aggregateId) =>
        AggregateId = aggregateId;
}
