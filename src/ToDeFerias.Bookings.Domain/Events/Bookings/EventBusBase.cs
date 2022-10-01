using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Domain.Events.Bookings;

public abstract class EventBusBase
{
    private readonly ITrace _trace;
    private readonly IMessageBus _messageBus;

    public EventBusBase(ITrace trace,
                       IMessageBus messageBus)
    {
        _trace = trace;
        _messageBus = messageBus;
    }

    protected void PublishEvent<TEvent>(TEvent @event, QueueSettings queue) where TEvent : Event
    {
        var eventWrapper = new EventWrapper(_trace.GetTraceId(),
                                            @event);

        _messageBus.Publish(eventWrapper, queue);
    }
}
