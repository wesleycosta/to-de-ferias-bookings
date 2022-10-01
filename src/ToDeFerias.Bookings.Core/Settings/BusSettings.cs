namespace ToDeFerias.Bookings.Core.Settings;

public sealed class BusSettings
{
    public string ConnectionString { get; set; }
    public QueueSettings Notifications { get; set; }
}
