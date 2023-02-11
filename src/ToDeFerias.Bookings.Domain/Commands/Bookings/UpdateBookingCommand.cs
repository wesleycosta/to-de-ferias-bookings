using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Commands.Bookings.Validations;
using ToDeFerias.Bookings.Domain.Inputs.Bookings;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class UpdateBookingCommand : Command
{
    public UpdateBookingInputModel InputModel { get; private set; }

    public UpdateBookingCommand(Guid bookingId, UpdateBookingInputModel inputModel)
    {
        AggregateId = bookingId;
        InputModel = inputModel;
    }

    public override bool IsValid() =>
        new UpdateBookingValidation()
            .Validate(this)
            .IsValid;
}
