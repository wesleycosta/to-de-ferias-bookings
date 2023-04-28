using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Bookings.Commands;
using ToDeFerias.Bookings.Domain.Bookings.Events;
using ToDeFerias.Bookings.Domain.HouseGuests.Commands;

namespace ToDeFerias.Bookings.Domain;

public static class DomainDependencyInjection
{
    public static IServiceCollection AddDomainConfiguration(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddEvents();

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddBookingCommands();
        services.AddHouseGuestCommands();

        return services;
    }

    private static IServiceCollection AddBookingCommands(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CancelBookingCommand, CommandHandlerResult>, CancelBookingCommandHandler>();
        services.AddScoped<IRequestHandler<CheckInCommand, CommandHandlerResult>, CheckInCommandHandler>();
        services.AddScoped<IRequestHandler<CheckOutCommand, CommandHandlerResult>, CheckOutCommandHandler>();
        services.AddScoped<IRequestHandler<RegisterBookingCommand, CommandHandlerResult>, RegisterBookingCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateBookingCommand, CommandHandlerResult>, UpdateBookingCommandHandler>();

        return services;
    }

    private static IServiceCollection AddHouseGuestCommands(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<RegisterHouseGuestCommand, CommandHandlerResult>, RegisterHouseGuestCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateHouseGuestCommand, CommandHandlerResult>, UpdateHouseGuestCommandHandler>();

        return services;
    }

    private static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler<RegisteredBookingEvent>, RegisteredBookingHandler>();

        return services;
    }
}
