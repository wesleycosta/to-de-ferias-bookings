using MediatR;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Core.Messages;

namespace ToDeFerias.Bookings.Infrastructure.Mediator;

public sealed class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator) =>
        _mediator = mediator;

    public async Task PublishEvent<T>(T eventMessage) where T : Event =>
        await _mediator.Publish(eventMessage);

    public async Task<CommandHandlerResult?> SendCommand<T>(T comando) where T : Command =>
        await _mediator.Send(comando);
}
