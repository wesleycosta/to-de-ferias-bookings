using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Commands.Bookings.Validations;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class CheckInCommand : Command
{
    public CheckInCommand(Guid aggregateId) =>
        AggregateId = aggregateId;

    public override bool IsValid() =>
        new CheckInCommandValidation()
            .Validate(this)
            .IsValid;
}
