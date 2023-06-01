using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Commands;

public class DeleteHouseGuestCommandHandler : CommandHandler, IRequestHandler<DeleteHouseGuestCommand, CommandHandlerResult>
{
    private readonly IHouseGuestRepository _houseGuestRepository;
    
    public DeleteHouseGuestCommandHandler(IHouseGuestRepository houseGuestRepository)
        => _houseGuestRepository = houseGuestRepository;

    public async Task<CommandHandlerResult> Handle(DeleteHouseGuestCommand request, CancellationToken cancellationToken)
    {
        var booking = await _houseGuestRepository.GetById(request.AggregateId);
        if (booking is null)
            return BadCommand("Booking not found");

        await _houseGuestRepository.Remove(request.AggregateId);
        return SuccessfulCommand();
    }
}
