using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Logger;
using ToDeFerias.Bookings.Api.Infrastructure.Mappers;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.CrossCutting.IoC;
using ToDeFerias.Bookings.Domain;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations;

internal static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenConvention = new RouteTokenTransformerConvention(new SlugifyParameterTransformer());
        services.AddControllers(options => options.Conventions.Add(tokenConvention));

        return services.AddEndpointsApiExplorer()
            .AddSwagger()
            .AddNotifications()
            .AddTraceLogger()
            .AddAppConfiguration()
            .AddSettings(configuration)
            .AddInfraConfiguration(configuration)
            .AddDomainConfiguration()
            .AddHttpContextAccessor();
    }

    private static IServiceCollection AddNotifications(this IServiceCollection services) =>
        services.AddScoped<INotifier, Notifier>();

    private static IServiceCollection AddTraceLogger(this IServiceCollection services) =>
        services.AddSingleton<ITrace, Trace>();

    public static IServiceCollection AddAppConfiguration(this IServiceCollection services) =>
        services.AddAutoMapper(config => config.AddProfile<DomainToDtoMappingProfile>());

    public static void UseApiConfiguration(this IApplicationBuilder app) =>
        app.UseSwaggerConfiguration()
           .UseHttpsRedirection()
           .UseMiddlewares()
           .UseAuthorization();
}
