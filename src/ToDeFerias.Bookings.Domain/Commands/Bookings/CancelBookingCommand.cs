using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Commands.Bookings.Validations;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class CancelBookingCommand : Command
{
    public CancelBookingCommand(Guid aggregateId) =>
        AggregateId = aggregateId;

    public override bool IsValid() =>
        new CancelBookingCommandValidation()
            .Validate(this)
            .IsValid;
}
