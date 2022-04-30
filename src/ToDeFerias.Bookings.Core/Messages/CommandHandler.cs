using FluentValidation.Results;
using ToDeFerias.Bookings.Core.Data;

namespace ToDeFerias.Bookings.Core.Messages;

public abstract class CommandHandler
{
    public ValidationResult ValidationResult;

    public CommandHandler() =>
        ValidationResult = new ValidationResult();

    public void NotifyError(string message) =>
        ValidationResult?.Errors.Add(new ValidationFailure(string.Empty, message));

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public async Task<CommandHandlerResult> SaveData(IUnitOfWork unitOfWork, object? responseCommand)
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    {
        if (!await unitOfWork.Commit())
        {
            NotifyError("Houve um erro ao persistir os dados");
            responseCommand = null;
        }

        return new()
        {
            ValidationResult = ValidationResult,
            Response = responseCommand
        };
    }

    public CommandHandlerResult Response() =>
        new()
        {
            ValidationResult = ValidationResult
        };

    public CommandHandlerResult BadCommand() =>
        Response();

    public CommandHandlerResult BadCommand(string message)
    {
        NotifyError(message);
        return BadCommand();
    }

    public CommandHandlerResult BadCommand(Command command)
    {
        ValidationResult = command.ValidationResult;
        return new()
        {
            ValidationResult = ValidationResult
        };
    }

    public CommandHandlerResult BadCommand(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
        return new()
        {
            ValidationResult = ValidationResult
        };
    }

    public CommandHandlerResult Response(object responseCommand) =>
        new()
        {
            ValidationResult = ValidationResult,
            Response = responseCommand
        };

    public static CommandHandlerResult Response(CommandHandlerResult commandHandlerResult) =>
        commandHandlerResult;
}
