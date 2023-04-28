using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Bookings.Inputs;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class UpdateBookingCommand : Command
{
    public UpdateBookingInputModel InputModel { get; private set; }

    public UpdateBookingCommand(Guid bookingId,
        UpdateBookingInputModel inputModel)
    {
        AggregateId = bookingId;
        InputModel = inputModel;
    }
}
