using RabbitMQ.Client;
using System.Text;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Infrastructure.Bus;

internal static class RabbitMqExtensions
{
    public static void ExchangeDirectDeclare(this IModel channel, QueueSettings queueSettings) =>
        channel.ExchangeDeclare(queueSettings.Exchange,
                                "direct",
                                true,
                                false);

    public static void QueueClassicDeclare(this IModel channel, QueueSettings queueSettings)
    {
        var arguments = new Dictionary<string, object> {
            { "x-queue-type", "classic" }
        };

        channel.QueueDeclare(queueSettings.Queue,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments);
    }

    public static void QueueBind(this IModel channel, QueueSettings queueSettings) =>
        channel.QueueBind(queueSettings.Queue,
                          queueSettings.Exchange,
                          queueSettings.RoutingKey);

    public static void PublishJson(this IModel channel, string json, QueueSettings queueSettings)
    {
        var properties = channel.CreateBasicProperties();
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(queueSettings.Exchange,
                             queueSettings.RoutingKey,
                             properties,
                             body);
    }
}
