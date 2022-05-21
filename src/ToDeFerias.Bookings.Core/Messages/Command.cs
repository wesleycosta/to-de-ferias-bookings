using FluentValidation.Results;
using MediatR;

namespace ToDeFerias.Bookings.Core.Messages;

public abstract class Command : Message, IRequest<CommandHandlerResult>
{
    public DateTimeOffset? Timestamp { get; private set; } = DateTimeOffset.Now;
    public ValidationResult ValidationResult { get; protected set; }

    public virtual bool IsValid() =>
        throw new NotImplementedException();
}
