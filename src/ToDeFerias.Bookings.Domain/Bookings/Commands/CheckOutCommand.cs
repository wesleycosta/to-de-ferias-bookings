using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class CheckOutCommand : Command
{
    public CheckOutCommand(Guid aggregateId) =>
        AggregateId = aggregateId;
}
