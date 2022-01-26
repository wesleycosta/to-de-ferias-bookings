using ToDeFerias.Bookings.Infrastructure;

namespace ToDeFerias.Bookings.Api.Infrastructure.Middlewares;

internal sealed class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _logger;

    public ErrorHandlerMiddleware(RequestDelegate next,
                                  ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.Error(exception);
        }
    }
}
