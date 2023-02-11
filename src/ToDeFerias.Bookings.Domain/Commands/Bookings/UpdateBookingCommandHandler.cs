using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Events.Bookings;
using ToDeFerias.Bookings.Domain.Inputs.Bookings;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class UpdateBookingCommandHandler : CommandHandler, IRequestHandler<UpdateBookingCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;

    public UpdateBookingCommandHandler(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public async Task<CommandHandlerResult> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return BadCommand(request);

        var booking = await _bookingRepository.GetById(request.AggregateId);
        if (booking is null)
            return BadCommand("Booking not found");

        if (!await RoomIsAvailable(booking, request.InputModel))
            return BadCommand();

        var commandResult = await UpdateBooking(booking, request.InputModel);
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
