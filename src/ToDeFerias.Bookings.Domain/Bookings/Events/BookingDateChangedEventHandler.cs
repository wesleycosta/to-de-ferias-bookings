using MediatR;
using Microsoft.Extensions.Options;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Domain.Bookings.Events;

public sealed class BookingDateChangedEventHandler : EventBusBase, INotificationHandler<BookingDateChangedEvent>
{
    private readonly BusSettings _messageBusSettings;

    public BookingDateChangedEventHandler(ITrace trace,
        IMessageBus messageBus,
        IOptions<BusSettings> messageBusSettings)
        : base(trace, messageBus)
        => _messageBusSettings = messageBusSettings.Value;

    public async Task Handle(BookingDateChangedEvent @event, CancellationToken cancellationToken)
    {
        PublishEvent(@event, _messageBusSettings.Notifications);
        await Task.CompletedTask;
    }
}
