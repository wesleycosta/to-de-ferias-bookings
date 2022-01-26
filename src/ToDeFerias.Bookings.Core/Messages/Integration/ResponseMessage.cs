using FluentValidation.Results;

namespace ToDeFerias.Bookings.Core.Messages.Integration;

public sealed class ResponseMessage : Message
{
    public ValidationResult ValidationResult { get; set; }

    public ResponseMessage(ValidationResult validationResult) =>
        ValidationResult = validationResult;
}
