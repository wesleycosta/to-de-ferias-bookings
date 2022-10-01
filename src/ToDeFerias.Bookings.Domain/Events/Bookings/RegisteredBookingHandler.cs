using MediatR;
using Microsoft.Extensions.Options;
using System.Text;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Domain.Events.Bookings;

public sealed class RegisteredBookingHandler : INotificationHandler<RegisteredBookingEvent>
{
    private readonly IMessageBus _messageBus;
    private readonly BusSettings _messageBusSettings;

    public RegisteredBookingHandler(IMessageBus messageBus,
                                    IOptions<BusSettings> messageBusSettings)
    {
        _messageBus = messageBus;
        _messageBusSettings = messageBusSettings.Value;
    }

    public async Task Handle(RegisteredBookingEvent @event, CancellationToken cancellationToken)
    {
        var message = FormatMessage(@event);
        var notification = new NotificationEvent("New Booking",
                                                 message,
                                                 @event.HouseGuestEmail.Address);

        await _messageBus.Publish(notification,
                                  _messageBusSettings.NotificationsQueueName);
    }

    private static string FormatMessage(RegisteredBookingEvent @event)
    {
        var builder = new StringBuilder();
        builder.Append($"Thank you {@event.HouseGuestName};\r\n");
        builder.Append($"New reservation made for room {@event.RoomTypeName} at time period {@event.DateRangeBooking}.");

        return builder.ToString();
    }
}
