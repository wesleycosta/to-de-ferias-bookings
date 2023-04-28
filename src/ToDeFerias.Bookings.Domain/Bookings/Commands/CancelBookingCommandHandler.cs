using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Bookings.Aggregates;
using ToDeFerias.Bookings.Domain.Bookings.Commands.Validations;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands;

public sealed class CancelBookingCommandHandler : CommandHandler, IRequestHandler<CancelBookingCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;

    public CancelBookingCommandHandler(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public async Task<CommandHandlerResult> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
    {
        if (!command.Validate(new CancelBookingCommandValidation(), command))
            return BadCommand();

        var booking = await _bookingRepository.GetById(command.AggregateId);
        if (booking is null)
            return BadCommand("Booking not found");

        if (!booking.IsItPossibleToCancel())
            return BadCommand("It is only possible to cancel for bookings that have the same status as Booked");

        booking.Cancel();
        _bookingRepository.Update(booking);
        var commandResult = await SaveData(_bookingRepository.UnitOfWork, booking);

        return SuccessfulCommand(commandResult);
    }
}
