using FluentValidation;
using ToDeFerias.Bookings.Core.Validations;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands.Validations;

public sealed class UpdateBookingValidation : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingValidation()
    {
        RuleFor(command => command.AggregateId)
            .NotEqual(Guid.Empty)
            .WithMessage(ValidationMessages.IdentifierIsInvalid());

        RuleFor(command => command.AggregateId)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.NotInformed("BookingId"));

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
