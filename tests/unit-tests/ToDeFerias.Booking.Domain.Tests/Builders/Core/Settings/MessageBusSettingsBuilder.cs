using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Core.Settings;

internal sealed class MessageBusSettingsBuilder : BaseBuilder<BusSettings, MessageBusSettingsBuilder>
{
    public override MessageBusSettingsBuilder BuildDefault()
    {
        Object = new BusSettings
        {
            NotificationsQueueName = "notifications-events",
        };

        return this;
    }
}
