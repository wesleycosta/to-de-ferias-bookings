using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Commands.Bookings.Validations;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class CheckOutCommand : Command
{
    public CheckOutCommand(Guid aggregateId) =>
        AggregateId = aggregateId;

    public override bool IsValid() =>
        new CheckOutCommandValidation()
            .Validate(this)
            .IsValid;
}
