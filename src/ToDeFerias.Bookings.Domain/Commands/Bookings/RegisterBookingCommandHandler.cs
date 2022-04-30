using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Core.Validations;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;
using ToDeFerias.Bookings.Domain.Events.Bookings;
using ToDeFerias.Bookings.Domain.Inputs.Bookings;
using ToDeFerias.Bookings.Domain.Repositories;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class RegisterBookingCommandHandler : CommandHandler, IRequestHandler<RegisterBookingCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IHouseGuestRepository _houseGuestRepository;

    public RegisterBookingCommandHandler(IBookingRepository bookingRepository,
                                         IHouseGuestRepository houseGuestRepository)
    {
        _bookingRepository = bookingRepository;
        _houseGuestRepository = houseGuestRepository;
    }

    public async Task<CommandHandlerResult> Handle(RegisterBookingCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return BadCommand(request);

        if (!await RoomIsAvailable(request.InputModel))
            return BadCommand();

        var commandHandlerResult = await SaveBooking(request.InputModel);
        return Response(commandHandlerResult);
    }

    private async Task<bool> RoomIsAvailable(RegisterBookingInputModel inputModel)
    {
        var dateRangeBooking = new DateRangeBooking(inputModel.CheckIn, inputModel.CheckOut);

        if (await _bookingRepository.ItsBooked(inputModel.RoomId, dateRangeBooking))
        {
            NotifyError($"There is already a booking registered in the period {dateRangeBooking}");
            return false;
        }

        return true;
    }

    private async Task<CommandHandlerResult> SaveBooking(RegisterBookingInputModel inputModel)
    {
        var houseGuest = await _houseGuestRepository.GetById(inputModel.HouseGuestId);
        if (houseGuest is null)
            return BadCommand(ValidationMessages.NotFoundInDatabase("HouseGuestId"));

        var room = await _bookingRepository.GetRoomById(inputModel.RoomId);
        if (room is null)
            return BadCommand(ValidationMessages.NotFoundInDatabase("RoomId"));

        var booking = AddBooking(inputModel);
        var @event = new RegisteredBookingEvent(houseGuest,
                                                room,
                                                booking.DateRange,
                                                booking.Value,
                                                booking.Adults,
                                                booking.Children);

        booking.AddEvent(@event);

        return await SaveData(_bookingRepository.UnitOfWork, booking);
    }

    private Booking AddBooking(RegisterBookingInputModel inputModel)
    {
        var booking = new Booking(inputModel.HouseGuestId,
                                  inputModel.RoomId,
                                  inputModel.CheckIn,
                                  inputModel.CheckOut,
                                  inputModel.Value,
                                  (byte)inputModel.Adults,
                                  (byte)inputModel.Children);

        _bookingRepository.Add(booking);
        return booking;
    }
}
