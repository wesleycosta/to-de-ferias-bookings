using FluentValidation;
using ToDeFerias.Bookings.Core.Validations;

namespace ToDeFerias.Bookings.Domain.Commands.Bookings.Validations;

public sealed class CheckOutCommandValidation : AbstractValidator<CheckOutCommand>
{
    public CheckOutCommandValidation() =>
        RuleFor(command => command.AggregateId)
           .NotEmpty()
           .NotNull()
           .WithMessage(ValidationMessages.TheFieldCannotBeEmpty("BookingId"));
}
