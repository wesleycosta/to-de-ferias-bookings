using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;
using ToDeFerias.Bookings.Domain.HouseGuests.Commands.Validations;
using ToDeFerias.Bookings.Domain.HouseGuests.Events;
using ToDeFerias.Bookings.Domain.HouseGuests.Inputs;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Commands;

public class UpdateHouseGuestCommandHandler : CommandHandler, IRequestHandler<UpdateHouseGuestCommand, CommandHandlerResult>
{
    private readonly IHouseGuestRepository _houseGuestRepository;

    public UpdateHouseGuestCommandHandler(IHouseGuestRepository houseGuestRepository)
        => _houseGuestRepository = houseGuestRepository;

    public async Task<CommandHandlerResult> Handle(UpdateHouseGuestCommand command, CancellationToken cancellationToken)
    {
        if (!command.Validate(new UpdateHouseGuestCommandValidation(), command))
            return BadCommand(command);

        var houseGuest = await _houseGuestRepository.GetById(command.AggregateId);
        if (houseGuest is null)
            return BadCommand("HouseGuest not found");

        if (await CpfAlreadyExists(command.InputModel.Cpf, command.AggregateId))
            return BadCommand("A houseguest already exists with the CPF entered");

        if (await EmailAlreadyExists(command.InputModel.Email, command.AggregateId))
            return BadCommand("A houseguest already exists with the e-mail entered");

        var commandResult = await UpdateHouseGuest(houseGuest, command.InputModel);
        return SuccessfulCommand(commandResult);
    }

    private async Task<bool> CpfAlreadyExists(string cpf, Guid aggregateId)
    {
        var houseGuestWithSameCpf = await _houseGuestRepository.GetByCpfAndIdIsDifferentFrom(cpf, aggregateId);
        return houseGuestWithSameCpf != null;
    }

    private async Task<bool> EmailAlreadyExists(string email, Guid aggregateId)
    {
        var houseGuestWithSameEmail = await _houseGuestRepository.GetByEmailAndIdIsDifferentFrom(email, aggregateId);
        return houseGuestWithSameEmail != null;
    }

    private async Task<CommandHandlerResult> UpdateHouseGuest(HouseGuest houseGuest, UpdateHouseGuestInputModel inputModel)
    {
        houseGuest.Update(inputModel.Name,
            inputModel.Email,
            inputModel.Cpf,
            inputModel.DateOfBirth);

        var @event = new UpdatedHouseGuestEvent(houseGuest.Id,
            houseGuest.Name,
            houseGuest.Email.Address);

        houseGuest.AddEvent(@event);
        _houseGuestRepository.Update(houseGuest);

        return await SaveData(_houseGuestRepository.UnitOfWork, houseGuest);
    }
}
