using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Core.Bus;

public interface IMessageBus
{
    void Publish(EventWrapper @event, QueueSettings queueSettings);
}
