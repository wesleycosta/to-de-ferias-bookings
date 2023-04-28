using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.HouseGuests.Inputs;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Commands;

public class RegisterHouseGuestCommand : Command
{
    public RegisterHouseGuestInputModel InputModel { get; private set; }

    public RegisterHouseGuestCommand(RegisterHouseGuestInputModel inputModel)
        => InputModel = inputModel;
}
