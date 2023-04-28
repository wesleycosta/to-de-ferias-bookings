using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.HouseGuests.Inputs;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Commands;

public class UpdateHouseGuestCommand : Command
{
    public UpdateHouseGuestInputModel InputModel { get; set; }

    public UpdateHouseGuestCommand(Guid aggregateId,
        UpdateHouseGuestInputModel inputModel)
    {
        AggregateId = aggregateId;
        InputModel = inputModel;
    }
}
