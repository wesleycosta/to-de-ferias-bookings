using ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations.Settings;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations;

internal static class SettingsConfigurations
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<ApiKeySetting>(configuration.GetSection("ApiKey"));
}
