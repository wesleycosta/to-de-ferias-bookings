using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Core.Bus;

public interface IMessageBus
{
    Task Publish<TEvent>(TEvent @event, string queue) where TEvent : Event;
}
