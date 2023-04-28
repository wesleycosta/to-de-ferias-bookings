using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Bookings.Inputs;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class RegisterBookingCommand : Command
{
    public RegisterBookingInputModel InputModel { get; private set; }

    public RegisterBookingCommand(RegisterBookingInputModel inputModel)
        => InputModel = inputModel;
}
