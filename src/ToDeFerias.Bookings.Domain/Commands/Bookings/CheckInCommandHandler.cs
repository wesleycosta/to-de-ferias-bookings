using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class CheckInCommandHandler : CommandHandler, IRequestHandler<CheckInCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;

    public CheckInCommandHandler(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public async Task<CommandHandlerResult> Handle(CheckInCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return BadCommand();

        var booking = await _bookingRepository.GetById(request.AggregateId);
        if (booking is null)
            return BadCommand("Booking not found");

        if (!booking.IsItPossibleToCheckIn())
            return BadCommand("It is only possible to check in for bookings that have the same status as Booked");

        booking.CheckIn();
        _bookingRepository.Update(booking);
        var commandResult = await SaveData(_bookingRepository.UnitOfWork, booking);

        return SuccessfulCommand(commandResult);
    }
}
