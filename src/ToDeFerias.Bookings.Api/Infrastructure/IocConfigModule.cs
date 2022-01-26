using ToDeFerias.Bookings.Api.Infrastructure.Mappers;
using ToDeFerias.Bookings.Domain;
using ToDeFerias.Bookings.Infrastructure;

namespace ToDeFerias.Bookings.Api.Infrastructure;

internal static class IocConfigModule
{
    public static IServiceCollection AddIocConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.AddInfraConfiguration(configuration)
                .AddDomainConfiguration()
                .AddAppConfiguration();

    public static IServiceCollection AddAppConfiguration(this IServiceCollection services) =>
        services.AddAutoMapper(config =>
        {
            config.AddProfile<DomainToDtoMappingProfile>();
            config.AddProfile<DtoToDomainMappingProfile>();
        });
}
