using MediatR;

namespace ToDeFerias.Bookings.Core.Messages;

public abstract class Event : Message, INotification
{
    public DateTimeOffset? Timestamp { get; private set; }

    public Event() =>
        Timestamp = DateTimeOffset.Now;
}
