using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Domain.Repositories;
using ToDeFerias.Bookings.Infrastructure.Context;
using ToDeFerias.Bookings.Infrastructure.Logger;
using ToDeFerias.Bookings.Infrastructure.Mediator;
using ToDeFerias.Bookings.Infrastructure.Repositories;

namespace ToDeFerias.Bookings.Infrastructure;

public static class InfraConfigModule
{
    public static IServiceCollection AddInfraConfiguration(this IServiceCollection services, IConfiguration configuration) =>
       services.AddBookingContext(configuration)
               .AddMediator()
               .AddRepositories();

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorHandler));
        return services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static IServiceCollection AddBookingContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<BookingContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddScoped<BookingContext>();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IBookingRepository, BookingRepository>()
                .AddScoped<IHouseGuestRepository, HouseGuestRepository>();

    public static IApplicationBuilder ApplyMigrate(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<BookingContext>();
        context?.Database?.Migrate();

        return app;
    }

    private static IServiceCollection AddLogger(this IServiceCollection services) =>
        services.AddScoped<ILoggerService, LoggerService>();
}
