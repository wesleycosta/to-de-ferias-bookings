using ToDeFerias.Bookings.Api.Infrastructure.Middlewares;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations;

internal static class MiddlewareConfiguration
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app) =>
        app.UseMiddleware<ErrorHandlerMiddleware>();
}
