using Serilog;
using ToDeFerias.Bookings.Domain.Services.Logger;

namespace ToDeFerias.Bookings.Infrastructure.Logger;

public sealed class LoggerService : ILoggerService
{
    private readonly ILogger _logger;
    private static readonly string _messageTemplateDefault = "operation={operation}; message={message};traceId={traceId};machine={machine};version={version}";

    public LoggerService(ILogger logger) =>
        _logger = logger;

    public void Information(string operation, string message, Guid? traceId = null) =>
        _logger.Information(_messageTemplateDefault,
                            operation,
                            message,
                            traceId,
                            GetMachineName(),
                            GetVersion());

    public void Information(string operation, string message, object body, Guid? traceId = null) =>
        _logger.Information(string.Concat(_messageTemplateDefault, ";body={body}"),
                            operation,
                            message,
                            traceId,
                            GetMachineName(),
                            GetVersion(),
                            body);

    public void Information(string operation, string message, object body, int statusCode, Guid? traceId = null) =>
     _logger.Information(string.Concat(_messageTemplateDefault, ";body={body};statusCode={statusCode}"),
                         operation,
                         message,
                         traceId,
                         GetMachineName(),
                         GetVersion(),
                         body,
                         statusCode);

    public void Error(string operation, string message, Exception exception, Guid? traceId = null) =>
    _logger.Error(string.Concat(_messageTemplateDefault, ";exception={exception}"),
                  operation,
                  message,
                  traceId,
                  GetMachineName(),
                  GetVersion(),
                  exception);

    public void Error(string operation, string message, Exception exception, object request, Guid? traceId = null) =>
        _logger.Error(string.Concat(_messageTemplateDefault, ";exception={exception};request={request}"),
              operation,
              message,
              traceId,
              GetMachineName(),
              GetVersion(),
              exception,
              request);

    public void CloseAndFlush() =>
        Log.CloseAndFlush();

    private static string GetMachineName() =>
        Environment.MachineName;

    private static string GetVersion() =>
        "1.0.0";
}
