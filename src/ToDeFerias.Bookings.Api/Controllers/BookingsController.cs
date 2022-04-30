using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDeFerias.Bookings.Api.DTOs;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Controllers;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Domain.Commands.Bookings;
using ToDeFerias.Bookings.Domain.Inputs.Bookings;

namespace ToDeFerias.Bookings.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BookingsController : MainController
{
    private readonly IMediatorHandler _mediator;
    private readonly IMapper _mapper;

    public BookingsController(IMapper mapper,
                              INotifier notifier,
                              IMediatorHandler mediatorHandler) : base(mapper, notifier)
    {
        _mapper = mapper;
        _mediator = mediatorHandler;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] RegisterBookingInputModel inputModel)
    {
        var command = new RegisterBookingCommand(inputModel);
        var result = await _mediator.SendCommand(command);

        return Created<BookingDto>(_mapper.Map<CommandHandlerResultDto>(result), "~api/bookings/1");
    }
}
