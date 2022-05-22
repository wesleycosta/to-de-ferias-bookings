using FluentValidation.Results;
using ToDeFerias.Bookings.Core.Data;

namespace ToDeFerias.Bookings.Core.Messages;

public abstract class CommandHandler
{
    public ValidationResult ValidationResult;

    public CommandHandler() =>
        ValidationResult = new ValidationResult();

    public void Notify(string message) =>
        ValidationResult?.Errors.Add(new ValidationFailure(string.Empty, message));

    public async Task<CommandHandlerResult> SaveData(IUnitOfWork unitOfWork, object responseCommand)
    {
        if (!await unitOfWork.Commit())
        {
            Notify("Houve um erro ao persistir os dados");
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
        Notify(message);
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

    public CommandHandlerResult SuccessfulCommand(object responseCommand) =>
        new()
        {
            ValidationResult = ValidationResult,
            Response = responseCommand
        };

    public static CommandHandlerResult SuccessfulCommand(CommandHandlerResult commandHandlerResult) =>
        commandHandlerResult;
}
