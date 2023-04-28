using FluentValidation;
using ToDeFerias.Bookings.Core.Validations;

namespace ToDeFerias.Bookings.Domain.Bookings.Commands.Validations;

public sealed class CancelBookingCommandValidation : AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidation() =>
        RuleFor(command => command.AggregateId)
            .NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.TheFieldCannotBeEmpty("BookingId"));
}
