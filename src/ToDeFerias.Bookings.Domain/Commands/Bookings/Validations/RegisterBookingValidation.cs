using FluentValidation;
using ToDeFerias.Bookings.Core.Validations;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings.Validations;

public sealed class RegisterBookingValidation : AbstractValidator<RegisterBookingCommand>
{
    public RegisterBookingValidation()
    {
        RuleFor(command => command.InputModel.HouseGuestId)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.NotInformed("HouseGuestId"));

        RuleFor(command => command.InputModel.RoomId)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.NotInformed("RoomId"));

        RuleFor(command => command.InputModel.CheckIn)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.NotInformed("CheckIn"));

        RuleFor(command => command.InputModel.CheckOut)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.NotInformed("CheckOut"));

        RuleFor(command => command.InputModel.Value)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.GreaterThan("Value"));

        RuleFor(command => command.InputModel.Adults)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.GreaterThan("Adults"));
    }
}
