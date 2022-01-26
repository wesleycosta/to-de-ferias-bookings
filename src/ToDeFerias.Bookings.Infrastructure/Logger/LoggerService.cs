using Serilog;

namespace ToDeFerias.Bookings.Infrastructure.Logger;

public sealed class LoggerService : ILoggerService
{
    private readonly ILogger _logger;

    public LoggerService(ILogger logger) =>
        _logger = logger;

    public void Error(Exception exception) =>
        _logger.Error("Exception={Exception}", exception);
}
