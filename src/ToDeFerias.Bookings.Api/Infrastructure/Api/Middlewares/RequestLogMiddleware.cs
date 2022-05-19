using ToDeFerias.Bookings.Core.Logger;

namespace ToDeFerias.Bookings.Api.Infrastructure.Middlewares;

internal sealed class RequestLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _logger;

    public RequestLogMiddleware(RequestDelegate next,
                                ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var traceId = Guid.NewGuid();
        var operation = $"{context.Request.Method} {context.Request.Path.Value}";

        await LogRequest(operation, traceId, context);

        try
        {
            await LogResponseAndInvokeNext(operation, traceId, context);
        }
        catch (Exception exception)
        {
            _logger.Error(operation, "Request error", exception, traceId);
        }
    }

    private async Task LogRequest(string operation, Guid traceId, HttpContext context)
    {
        var body = await ReadRequestBody(context);
        context.Response.Headers.Add("TraceId", traceId.ToString());

        _logger.Information(operation, "Request received", body, traceId);
    }

    private static async Task<string> ReadRequestBody(HttpContext context)
    {
        context?.Request?.EnableBuffering();
        var streamReader = new StreamReader(context?.Request.Body);
        var body = await streamReader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        return body;
    }

    private async Task LogResponseAndInvokeNext(string operation, Guid traceId, HttpContext context)
    {
        using var buffer = new MemoryStream();
        var stream = context.Response.Body;
        context.Response.Body = buffer;

        await _next.Invoke(context);

        buffer.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(buffer);
        using var bufferReader = new StreamReader(buffer);
        var body = await bufferReader.ReadToEndAsync();

        buffer.Seek(0, SeekOrigin.Begin);

        await buffer.CopyToAsync(stream);
        context.Response.Body = stream;

        _logger.Information(operation,
                            "Response",
                            body,
                            context.Response.StatusCode,
                            traceId);
    }
}
