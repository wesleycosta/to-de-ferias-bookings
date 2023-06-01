using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDeFerias.Bookings.Api.Dtos;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Controllers;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;
using ToDeFerias.Bookings.Domain.HouseGuests.Commands;
using ToDeFerias.Bookings.Domain.HouseGuests.Inputs;

namespace ToDeFerias.Bookings.Api.Controllers;

[ApiController]
[Route("api/houseguests")]
public class HouseGuestsController : MainController
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;
    private readonly IHouseGuestRepository _houseGuestRepository;

    public HouseGuestsController(IMapper mapper,
        INotifier notifier,
        IMediatorHandler mediator,
        IHouseGuestRepository houseGuestRepository) : base(mapper, notifier)
    {
        _mapper = mapper;
        _mediator = mediator;
        _houseGuestRepository = houseGuestRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(HouseGuestFullDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] RegisterHouseGuestInputModel inputModel)
    {
        var command = new RegisterHouseGuestCommand(inputModel);
        var result = await _mediator.SendCommand(command);

        return Created<HouseGuestFullDto>(_mapper.Map<CommandHandlerResultDto>(result), "~api/houseguests/1");
    }

    [HttpPatch("{houseguestId}")]
    [ProducesResponseType(typeof(HouseGuestFullDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(Guid houseguestId, [FromBody] UpdateHouseGuestInputModel inputModel)
    {
        var command = new UpdateHouseGuestCommand(houseguestId, inputModel);
        var result = await _mediator.SendCommand(command);

        return Ok<HouseGuestFullDto>(_mapper.Map<CommandHandlerResultDto>(result));
    }

    [HttpDelete("{houseGuestId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(Guid houseGuestId)
    {
        var command = new DeleteHouseGuestCommand(houseGuestId);
        var result = await _mediator.SendCommand(command);

        return Ok<HouseGuestFullDto>(_mapper.Map<CommandHandlerResultDto>(result));
    }

    [HttpGet("{houseGuestId}")]
    [ProducesResponseType(typeof(HouseGuestFullDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(Guid houseGuestId)
    {
        var booking = await _houseGuestRepository.GetById(houseGuestId);
        if (booking is null)
            return NotFound();

        return Ok(_mapper.Map<HouseGuestFullDto>(booking));
    }
}
