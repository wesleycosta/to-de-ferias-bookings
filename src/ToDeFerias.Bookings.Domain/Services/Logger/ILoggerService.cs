namespace ToDeFerias.Bookings.Domain.Services.Logger;

public interface ILoggerService
{
    void Information(string operation, string message, Guid? traceId = null);
    void Information(string operation, string message, object body, Guid? traceId = null);
    void Information(string operation, string message, object body, int statusCode, Guid? traceId = null);
    void Error(string operation, string message, Exception exception, Guid? traceId = null);
    void Error(string operation, string message, Exception exception, object request, Guid? traceId = null);
    void CloseAndFlush();
}
