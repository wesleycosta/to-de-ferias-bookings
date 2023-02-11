using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDeFerias.Bookings.Api.Dtos;
using ToDeFerias.Bookings.Api.Dtos.Booking;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Controllers;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;
using ToDeFerias.Bookings.Core.Mediator;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Commands.Bookings;
using ToDeFerias.Bookings.Domain.Inputs.Bookings;

namespace ToDeFerias.Bookings.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BookingsController : MainController
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediator;
    private readonly IBookingRepository _bookingRepository;

    public BookingsController(IMapper mapper,
        INotifier notifier,
        IMediatorHandler mediatorHandler,
        IBookingRepository bookingRepository) : base(mapper, notifier)
    {
        _mapper = mapper;
        _mediator = mediatorHandler;
        _bookingRepository = bookingRepository;
    }

    [HttpGet("{bookingId}")]
    [ProducesResponseType(typeof(BookingFullDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(Guid bookingId)
    {
        var booking = await _bookingRepository.GetById(bookingId);
        if (booking is null)
            return NotFound();

        return Ok(_mapper.Map<BookingFullDto>(booking));
    }

    [HttpGet("start/{start}/end/{end}")]
    [ProducesResponseType(typeof(BookingFullDto[]), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetDateRange(DateTimeOffset start,
                                                  DateTimeOffset end)
    {
        var bookings = await _bookingRepository.GetDateRange(start, end);
        return Ok(_mapper.Map<BookingFullDto[]>(bookings));
    }

    [HttpGet("room/{roomId}/start/{start}/end/{end}")]
    [ProducesResponseType(typeof(BookingFullDto[]), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetByRoomId(Guid roomId,
                                                 DateTimeOffset start,
                                                 DateTimeOffset end)
    {
        var bookings = await _bookingRepository.GetByRoomId(roomId, start, end);
        return Ok(_mapper.Map<BookingFullDto[]>(bookings));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] RegisterBookingInputModel inputModel)
    {
        var command = new RegisterBookingCommand(inputModel);
        var result = await _mediator.SendCommand(command);

        return Created<BookingDto>(_mapper.Map<CommandHandlerResultDto>(result), "~api/bookings/1");
    }

    [HttpPatch("{bookingId}")]
    [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(Guid bookingId, [FromBody] UpdateBookingInputModel inputModel)
    {
        var command = new UpdateBookingCommand(bookingId, inputModel);
        var result = await _mediator.SendCommand(command);

        return Ok<BookingDto>(_mapper.Map<CommandHandlerResultDto>(result));
    }

    [HttpPatch("{bookingId}/check-in")]
    [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CheckIn(Guid bookingId)
    {
        var command = new CheckInCommand(bookingId);
        var result = await _mediator.SendCommand(command);

        return Ok<BookingDto>(_mapper.Map<CommandHandlerResultDto>(result));
    }

    [HttpPatch("{bookingId}/check-out")]
    [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CheckOut(Guid bookingId)
    {
        var command = new CheckOutCommand(bookingId);
        var result = await _mediator.SendCommand(command);

        return Ok<BookingDto>(_mapper.Map<CommandHandlerResultDto>(result));
    }

    [HttpPatch("{bookingId}/cancel")]
    [ProducesResponseType(typeof(BookingDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Cancel(Guid bookingId)
    {
        var command = new CancelBookingCommand(bookingId);
        var result = await _mediator.SendCommand(command);

        return Ok<BookingDto>(_mapper.Map<CommandHandlerResultDto>(result));
    }
}
