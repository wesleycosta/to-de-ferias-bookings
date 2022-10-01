using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Commands.Bookings;
using ToDeFerias.Bookings.Domain.Events.Bookings;

namespace ToDeFerias.Bookings.Domain;

public static class DomainIoC
{
    public static IServiceCollection AddDomainConfiguration(this IServiceCollection services) =>
        services.AddCommands()
                .AddEvents();

    private static IServiceCollection AddCommands(this IServiceCollection services) =>
        services.AddScoped<IRequestHandler<CancelBookingCommand, CommandHandlerResult>, CancelBookingCommandHandler>()
                .AddScoped<IRequestHandler<CheckInCommand, CommandHandlerResult>, CheckInCommandHandler>()
                .AddScoped<IRequestHandler<CheckOutCommand, CommandHandlerResult>, CheckOutCommandHandler>()
                .AddScoped<IRequestHandler<RegisterBookingCommand, CommandHandlerResult>, RegisterBookingCommandHandler>()
                .AddScoped<IRequestHandler<UpdateBookingCommand, CommandHandlerResult>, UpdateBookingCommandHandler>();

    private static IServiceCollection AddEvents(this IServiceCollection services) =>
       services.AddScoped<INotificationHandler<RegisteredBookingEvent>, RegisteredBookingHandler>();
}
