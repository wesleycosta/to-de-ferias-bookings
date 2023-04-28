using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Bookings.Aggregates;
using ToDeFerias.Bookings.Domain.Bookings.Commands.Validations;
using ToDeFerias.Bookings.Domain.Bookings.Events;
using ToDeFerias.Bookings.Domain.Bookings.Inputs;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class UpdateBookingCommandHandler : CommandHandler, IRequestHandler<UpdateBookingCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;

    public UpdateBookingCommandHandler(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public async Task<CommandHandlerResult> Handle(UpdateBookingCommand command, CancellationToken cancellationToken)
    {
        if (!command.Validate(new UpdateBookingValidation(), command))
            return BadCommand(command);

        var booking = await _bookingRepository.GetById(command.AggregateId);
        if (booking is null)
            return BadCommand("Booking not found");

        if (!await RoomIsAvailable(booking, command.InputModel))
            return BadCommand();

        var commandResult = await UpdateBooking(booking, command.InputModel);
        return SuccessfulCommand(commandResult);
    }

    private async Task<bool> RoomIsAvailable(Booking booking, UpdateBookingInputModel inputModel)
    {
        var dateRange = new DateRangeBooking(inputModel.CheckIn, inputModel.CheckOut);
        if (!booking.HasDateChanged(dateRange))
            return true;

        if (await _bookingRepository.ItsBooked(booking.Id, booking.RoomId, dateRange.CheckIn, dateRange.CheckOut))
        {
            Notify($"There is already a booking registered in the period {dateRange}");
            return false;
        }

        var @event = new BookingDateChangedEvent(booking.HouseGuest,
            booking.Room,
            booking.Period,
            dateRange);

        booking.AddEvent(@event);

        return true;
    }

    private async Task<CommandHandlerResult> UpdateBooking(Booking booking, UpdateBookingInputModel inputModel)
    {
        booking.Update(inputModel.Value,
            (byte)inputModel.Adults,
            (byte)inputModel.Children,
            inputModel.CheckIn,
            inputModel.CheckOut);

        _bookingRepository.Update(booking);

        return await SaveData(_bookingRepository.UnitOfWork, booking);
    }
}
