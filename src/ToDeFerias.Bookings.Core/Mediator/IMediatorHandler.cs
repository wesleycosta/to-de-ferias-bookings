using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Core.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T eventMessage) where T : Event;
    Task<CommandHandlerResult> SendCommand<T>(T command) where T : Command;
}
