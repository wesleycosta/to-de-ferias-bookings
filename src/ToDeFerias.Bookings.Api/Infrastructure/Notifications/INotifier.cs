namespace ToDeFerias.Bookings.Api.Infrastructure.Notifications;

public interface INotifier
{
    bool HasNotification();
    IReadOnlyList<Notification> GetNotifications();
    void Send(Notification notification);
    void Send(string message);
}
