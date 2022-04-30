namespace ToDeFerias.Bookings.Api.Infrastructure.Notifications;

public sealed class Notification
{
    public string Message { get; }

    public Notification(string message) =>
        Message = message;
}
