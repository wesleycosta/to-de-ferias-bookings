namespace ToDeFerias.Bookings.Core.Settings;

public sealed class QueueSettings
{
    public string Queue { get; set; }
    public string Exchange { get; set; }
    public string RoutingKey { get; set; }
}
