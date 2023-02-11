using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Commands.Bookings.Validations;
using ToDeFerias.Bookings.Domain.Inputs.Bookings;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class RegisterBookingCommand : Command
{
    public RegisterBookingInputModel InputModel { get; private set; }

    public RegisterBookingCommand(RegisterBookingInputModel inputModel) =>
        InputModel = inputModel;

    public override bool IsValid() =>
        new RegisterBookingValidation()
            .Validate(this)
            .IsValid;
}
