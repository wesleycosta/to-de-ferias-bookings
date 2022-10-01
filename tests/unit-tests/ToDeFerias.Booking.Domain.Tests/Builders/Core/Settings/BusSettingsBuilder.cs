using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Core.Settings;

internal sealed class BusSettingsBuilder : BaseBuilder<BusSettings, BusSettingsBuilder>
{
    public override BusSettingsBuilder BuildDefault()
    {
        Object = new BusSettings
        {
            Notifications = new QueueSettings
            {
                Queue = "notifications-events",
                Exchange = "notifications-exchange",
                RoutingKey = "notification-routing-key"
            }
        };

        return this;
    }
}
