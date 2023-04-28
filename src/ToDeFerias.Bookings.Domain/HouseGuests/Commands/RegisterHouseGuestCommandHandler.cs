using MediatR;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;
using ToDeFerias.Bookings.Domain.HouseGuests.Commands.Validations;
using ToDeFerias.Bookings.Domain.HouseGuests.Events;
using ToDeFerias.Bookings.Domain.HouseGuests.Inputs;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Commands;

public class RegisterHouseGuestCommandHandler : CommandHandler, IRequestHandler<RegisterHouseGuestCommand, CommandHandlerResult>
{
    private readonly IHouseGuestRepository _houseGuestRepository;

    public RegisterHouseGuestCommandHandler(IHouseGuestRepository houseGuestRepository)
        => _houseGuestRepository = houseGuestRepository;

    public async Task<CommandHandlerResult> Handle(RegisterHouseGuestCommand command, CancellationToken cancellationToken)
    {
        if (!command.Validate(new RegisterHouseGuestCommandValidation(), command))
            return BadCommand(command);

        if (await CpfAlreadyExists(command.InputModel.Cpf))
            return BadCommand("A houseguest already exists with the CPF entered");

        if (await EmailAlreadyExists(command.InputModel.Email))
            return BadCommand("A houseguest already exists with the e-mail entered");

        return await SaveHouseGuest(command.InputModel);
    }

    private async Task<bool> CpfAlreadyExists(string cpf)
    {
        var houseGuestWithSameCpf = await _houseGuestRepository.GetByCpf(cpf);
        return houseGuestWithSameCpf != null;
    }

    private async Task<bool> EmailAlreadyExists(string email)
    {
        var houseGuestWithSameEmail = await _houseGuestRepository.GetByEmail(email);
        return houseGuestWithSameEmail != null;
    }

    private async Task<CommandHandlerResult> SaveHouseGuest(RegisterHouseGuestInputModel inputModel)
    {
        var houseGuest = new HouseGuest(inputModel.Name,
            inputModel.Email,
            inputModel.Cpf,
            inputModel.DateOfBirth);

        var @event = new RegisteredHouseguestEvent(houseGuest.Id,
            houseGuest.Name,
            houseGuest.Email.Address);

        houseGuest.AddEvent(@event);
        _houseGuestRepository.Add(houseGuest);

        return await SaveData(_houseGuestRepository.UnitOfWork, houseGuest);
    }
}
