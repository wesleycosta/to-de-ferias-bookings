using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Logger;
using ToDeFerias.Bookings.Api.Infrastructure.Mappers;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.Domain;
using ToDeFerias.Bookings.Infrastructure;

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
                       .AddAppConfiguration()
                       .AddSettings(configuration)
                       .AddInfraConfiguration(configuration)
                       .AddDomainConfiguration()
                       .AddHttpContextAccessor();
    }

    private static IServiceCollection AddNotifications(this IServiceCollection services) =>
         services.AddScoped<INotifier, Notifier>();

    private static IServiceCollection AddTraceLogger(this IServiceCollection services) =>
        services.AddSingleton<ITraceLogger, TraceLogger>();

    public static IServiceCollection AddAppConfiguration(this IServiceCollection services) =>
      services.AddAutoMapper(config =>
      {
          config.AddProfile<DomainToDtoMappingProfile>();
          config.AddProfile<DtoToDomainMappingProfile>();
      });

    public static void UseApiConfiguration(this IApplicationBuilder app) =>
            app.UseSwaggerConfiguration()
               .UseHttpsRedirection()
               .UseMiddlewares()
               .UseAuthorization();
}
