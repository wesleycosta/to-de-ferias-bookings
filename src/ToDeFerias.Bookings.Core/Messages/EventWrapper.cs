namespace ToDeFerias.Bookings.Core.Messages;

public sealed class EventWrapper
{
    public string Type { get; private set; }
    public Guid TraceId { get; set; }
    public object Event { get; private set; }

    public EventWrapper(Guid traceId,
                        object @event)
    {
        TraceId = traceId;
        Event = @event;

        Type = @event.GetType().Name;
    }
}
