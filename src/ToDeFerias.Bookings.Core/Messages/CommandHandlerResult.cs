using FluentValidation.Results;

namespace ToDeFerias.Bookings.Core.Messages;

public sealed class CommandHandlerResult
{
    public ValidationResult ValidationResult { get; init; }
    public object Response { get; init; }

    public bool IsValid =>
        ValidationResult?.Errors?.Count == 0;
}
