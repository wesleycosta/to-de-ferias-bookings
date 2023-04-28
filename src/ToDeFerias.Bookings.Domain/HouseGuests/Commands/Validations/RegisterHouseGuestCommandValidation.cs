using FluentValidation;
using NetDevPack.Brasil.Documentos.Validacao;
using NetDevPack.Utilities;
using ToDeFerias.Bookings.Core.Validations;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;
using ToDeFerias.Bookings.Domain.HouseGuests.Commands;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Commands.Validations;

public class RegisterHouseGuestCommandValidation : AbstractValidator<RegisterHouseGuestCommand>
{
    public RegisterHouseGuestCommandValidation()
    {
        RuleFor(command => command.InputModel.Name)
           .NotNull()
           .NotEmpty()
           .WithMessage(ValidationMessages.NotInformed("Name"));

        RuleFor(command => command.InputModel.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage(ValidationMessages.IsInvalid("Email"));

        RuleFor(command => command.InputModel.Cpf)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.NotInformed("Cpf"));

        RuleFor(command => command.InputModel.Cpf)
            .Must(command => new CpfValidador(command.OnlyNumbers()).EstaValido())
            .WithMessage(ValidationMessages.IsInvalid("Cpf"));

        RuleFor(command => command.InputModel.DateOfBirth)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.NotInformed("DateOfBirth"));

        RuleFor(command => command.InputModel.DateOfBirth)
            .Must(DateOfBirth.IsValid)
            .WithMessage(ValidationMessages.IsInvalid("DateOfBirth"));
    }
}
