using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class CancelBookingCommandHandler : CommandHandler, IRequestHandler<CancelBookingCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;

    public CancelBookingCommandHandler(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public async Task<CommandHandlerResult> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return BadCommand();

        var booking = await _bookingRepository.GetById(request.AggregateId);
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
