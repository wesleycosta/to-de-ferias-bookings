using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings;

public sealed class CheckOutCommandHandler : CommandHandler, IRequestHandler<CheckOutCommand, CommandHandlerResult>
{
    private readonly IBookingRepository _bookingRepository;

    public CheckOutCommandHandler(IBookingRepository bookingRepository) =>
        _bookingRepository = bookingRepository;

    public async Task<CommandHandlerResult> Handle(CheckOutCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return BadCommand();

        var booking = await _bookingRepository.GetById(request.AggregateId);
        if (booking is null)
            return BadCommand("Booking not found");

        if (!booking.IsItPossibleToCheckOut())
            return BadCommand("It is only possible to check out for bookings that have the same status as CheckIn");

        booking.CheckOut();
        _bookingRepository.Update(booking);
        var commandResult = await SaveData(_bookingRepository.UnitOfWork, booking);

        return SuccessfulCommand(commandResult);
    }
}
