using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Bookings.Aggregates;
using ToDeFerias.Bookings.Domain.Bookings.Commands.Validations;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class CheckInCommandHandler : CommandHandler, IRequestHandler<CheckInCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;

    public CheckInCommandHandler(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public async Task<CommandHandlerResult> Handle(CheckInCommand command, CancellationToken cancellationToken)
    {
        if (!command.Validate(new CheckInCommandValidation(), command))
            return BadCommand();

        var booking = await _bookingRepository.GetById(command.AggregateId);
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
