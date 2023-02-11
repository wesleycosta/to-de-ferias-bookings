using MediatR;
using Microsoft.Extensions.Options;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Domain.Events.Bookings;

public sealed class RegisteredBookingHandler : EventBusBase, INotificationHandler<RegisteredBookingEvent>
{
    private readonly BusSettings _messageBusSettings;

    public RegisteredBookingHandler(ITrace trace,
        IMessageBus messageBus,
        IOptions<BusSettings> messageBusSettings)
        : base(trace, messageBus)
        => _messageBusSettings = messageBusSettings.Value;

    public async Task Handle(RegisteredBookingEvent @event, CancellationToken cancellationToken)
    {
        PublishEvent(@event, _messageBusSettings.Notifications);
        await Task.CompletedTask;
    }
}
