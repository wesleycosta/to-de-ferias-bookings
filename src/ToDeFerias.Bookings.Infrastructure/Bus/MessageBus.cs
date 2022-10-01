using EasyNetQ;
using Microsoft.Extensions.Options;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Infrastructure.Bus;

public sealed class MessageBus : IMessageBus
{
    private readonly BusSettings _settings;

    public MessageBus(IOptions<BusSettings> settings) =>
        _settings = settings.Value;

    public async Task Publish<TEvent>(TEvent @event, string queue) where TEvent : Event
    {
        var bus = RabbitHutch.CreateBus(_settings.ConnectionString);
        await bus.PubSub.PublishAsync(@event, queue);
    }
}
