using ToDeFerias.Bookings.Core.Logger;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Logger;

public sealed class TraceLogger : ITraceLogger
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TraceLogger(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public Guid GetTraceId()
    {
        var traceId = _httpContextAccessor.HttpContext
                                          .Response
                                          .Headers["TraceId"];

        if (Guid.TryParse(traceId, out _))
            return Guid.Parse(traceId);

        return Guid.Empty;
    }
}
