using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Logger;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;
using ToDeFerias.Bookings.Domain.Services.Logger;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations;

internal static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));

        return services.AddEndpointsApiExplorer()
                       .AddSwagger()
                       .AddNotifications()
                       .AddTraceLogger()
                       .AddSettings(configuration)
                       .AddHttpContextAccessor();
    }

    private static IServiceCollection AddNotifications(this IServiceCollection services) =>
         services.AddScoped<INotifier, Notifier>();

    private static IServiceCollection AddTraceLogger(this IServiceCollection services) =>
        services.AddSingleton<ITraceLogger, TraceLogger>();

    public static void UseApiConfiguration(this IApplicationBuilder app) =>
        app.UseSwaggerConfiguration()
           .UseHttpsRedirection()
           .UseMiddlewares()
           .UseAuthorization();
}
