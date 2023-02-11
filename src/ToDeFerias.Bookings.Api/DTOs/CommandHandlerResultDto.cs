using FluentValidation.Results;

namespace ToDeFerias.Bookings.Api.Dtos;

public sealed class CommandHandlerResultDto
{
    public ValidationResult ValidationResult { get; set; }
    public object Response { get; set; }

    public bool IsValid =>
        ValidationResult?.Errors?.Count == 0;
}
