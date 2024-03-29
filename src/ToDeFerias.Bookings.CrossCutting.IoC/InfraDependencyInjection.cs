﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Domain.Bookings.Aggregates;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;
using ToDeFerias.Bookings.Infrastructure.Bus;
using ToDeFerias.Bookings.Infrastructure.Context;
using ToDeFerias.Bookings.Infrastructure.Data.Repositories;
using ToDeFerias.Bookings.Infrastructure.Logger;
using ToDeFerias.Bookings.Infrastructure.Mediator;

namespace ToDeFerias.Bookings.CrossCutting.IoC;

public static class InfraDependencyInjection
{
    public static IServiceCollection AddInfraConfiguration(this IServiceCollection services, IConfiguration configuration) =>
       services.AddLogger(configuration)
            .AddContext(configuration)
            .AddMediator()
            .AddRepositories()
            .AddMessageBus();

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorHandler));
        return services.AddScoped<IMediatorHandler, MediatorHandler>();
    }

    private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration) =>
       services.AddDbContext<BookingsContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IBookingRepository, BookingRepository>()
            .AddScoped<IHouseGuestRepository, HouseGuestRepository>();

    public static IApplicationBuilder ApplyMigrate(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices.GetService<ILoggerService>();
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            logger.Information("Startup", "Applying migrations");

            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<BookingsContext>();

            ArgumentNullException.ThrowIfNull(context);
            var pendingMigrations = context.Database.GetPendingMigrations();
            LogMigrations(logger, pendingMigrations);

            context.Database.Migrate();
        }
        catch (Exception exception)
        {
            logger.Error("Startup", "Failed to apply migrations", exception);
            logger.CloseAndFlush();

            throw;
        }

        return app;
    }

    private static void LogMigrations(ILoggerService logger, IEnumerable<string> pendingMigrations)
    {
        if (!pendingMigrations.Any())
        {
            logger.Information("Startup", "No pending migration");
            return;
        }

        logger.Information("Startup", $"Migration applied: {string.Join(", ", pendingMigrations)}");
    }

    public static IApplicationBuilder AddStartupAndShutdownLog(this IApplicationBuilder app)
    {
        var logger = app.ApplicationServices.GetService<ILoggerService>();
        ArgumentNullException.ThrowIfNull(logger);
        logger.Information("Startup", "Application started");

        AppDomain.CurrentDomain.ProcessExit += (s, e) =>
        {
            logger.Information("Shutdown", "Application finished");
            logger.CloseAndFlush();
        };

        return app;
    }

    private static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSingleton<ILoggerService, LoggerService>()
            .AddSerilog(configuration);

    private static IServiceCollection AddMessageBus(this IServiceCollection services) =>
        services.AddScoped<IMessageBus, MessageBus>();
}
