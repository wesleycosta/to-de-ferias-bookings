using FluentValidation.Results;
using MediatR;

namespace ToDeFerias.Bookings.Core.Messages;

public abstract class Command : Message, IRequest<CommandHandlerResult>
{
    public DateTime? Timestamp { get; private set; } = DateTime.Now;
    public ValidationResult ValidationResult { get; protected set; }

    public virtual bool IsValid() =>
        throw new NotImplementedException();
}
