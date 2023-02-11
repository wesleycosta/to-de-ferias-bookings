using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Core.Settings;

namespace ToDeFerias.Bookings.Infrastructure.Bus;

public sealed class MessageBus : IMessageBus
{
    private readonly BusSettings _busSettings;
    private readonly ILoggerService _loggerService;

    private Guid _traceId;
    private readonly Policy _retryPolicy;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly string _operation = "PublishQueue";
    private readonly int _retryCount = 3;

    public MessageBus(IOptions<BusSettings> busSettings,
            ILoggerService loggerService)
    {
        _busSettings = busSettings.Value;
        _loggerService = loggerService;

        _retryPolicy = Policy.Handle<BrokerUnreachableException>()
                             .Retry(_retryCount, onRetry: (exception, retryCount) =>
                             {
                                 _loggerService.Error(_operation, "Error when publishing event", exception, _traceId);
                                 _loggerService.Information(_operation, $"Re-publishing, attempt {retryCount} of {_retryCount}", _traceId);
                             });

        _jsonSerializerOptions = new JsonSerializerOptions();
        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }

    public void Publish(EventWrapper @event, QueueSettings queueSettings) =>
        _retryPolicy.Execute(() => TryPublish(@event, queueSettings));

    private void TryPublish(EventWrapper @event, QueueSettings queueSettings)
    {
        _traceId = @event.TraceId;

        var factory = new ConnectionFactory();
        factory.Uri = new Uri(_busSettings.ConnectionString);

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDirectDeclare(queueSettings);
        channel.QueueClassicDeclare(queueSettings);
        channel.QueueBind(queueSettings);

        var json = JsonSerializer.Serialize(@event, _jsonSerializerOptions);
        channel.PublishJson(json, queueSettings);

        _loggerService.Information(_operation, "Event published successfully", json, @event.TraceId);
    }
}
