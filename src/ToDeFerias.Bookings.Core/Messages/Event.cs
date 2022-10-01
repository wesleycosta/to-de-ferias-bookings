using MediatR;

namespace ToDeFerias.Bookings.Core.Messages;

public abstract class Event : INotification
{
    public Guid? AggregateId { get; protected set; }
    public DateTimeOffset? Timestamp { get; private set; }

    public Event() =>
        Timestamp = DateTimeOffset.Now;
}
