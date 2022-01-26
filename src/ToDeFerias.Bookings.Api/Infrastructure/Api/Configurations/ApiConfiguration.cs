using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations;

internal static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));

        return services.AddEndpointsApiExplorer()
                       .AddSwagger()
                       .AddNotifications()
                       .AddSettings(configuration);
    }

    public static IServiceCollection AddNotifications(this IServiceCollection services) =>
         services.AddScoped<INotifier, Notifier>();

    public static void UseApiConfiguration(this IApplicationBuilder app) =>
        app.UseSwaggerConfiguration()
           .UseHttpsRedirection()
           .UseAuthorization();
}
