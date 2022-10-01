using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Domain.Events.Bookings;

public sealed class NotificationEvent : Event
{
    public string Subject { get; private set; }
    public string Message { get; private set; }
    public string Addressee { get; private set; }

    public NotificationEvent(string subject,
                             string message,
                             string addressee)
    {
        Subject = subject;
        Message = message;
        Addressee = addressee;
    }
}
