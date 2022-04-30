using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDeFerias.Bookings.Api.DTOs;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;
using ToDeFerias.Bookings.Api.Infrastructure.Notifications;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Controllers;

[ApiKey]
public abstract class MainController : ControllerBase
{
    private readonly IMapper _mapper;
    public readonly INotifier Notifier;

    protected MainController(IMapper mapper,
                             INotifier notifier)
    {
        _mapper = mapper;
        Notifier = notifier;
    }

    protected bool IsValid() =>
        !Notifier.HasNotification();

    protected IActionResult Created<T>(CommandHandlerResultDto result, string url) where T : class, new() =>
         CustomResponse<T>(result, HttpStatusCode.Created, url);

    protected IActionResult Ok<T>(CommandHandlerResultDto result) where T : class, new() =>
        CustomResponse<T>(result, HttpStatusCode.OK);

    private IActionResult CustomResponse<T>(CommandHandlerResultDto result,
                                            HttpStatusCode statusCode = HttpStatusCode.OK,
                                            string url = "") where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(result);

        if (!result.IsValid)
            return CustomResponse<T>(result.ValidationResult);

        return CustomResponse<T>(result.Response, statusCode, url);
    }

    private IActionResult CustomResponse<T>(ValidationResult validationResult) where T : class, new()
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        foreach (var erro in validationResult.Errors)
            NotifyError(erro.ErrorMessage);

        return CustomResponse<T>();
    }

    private IActionResult CustomResponse<T>(object result = null,
                                            HttpStatusCode statusCode = HttpStatusCode.OK,
                                            string url = "") where T : class, new()
    {
        if (!IsValid())
            return BadRequestResponse();

        if (result == null)
            return NoContent();

        return HttpStatusCodeRespose<T>(_mapper.Map<T>(result), statusCode, url);
    }

    private IActionResult BadRequestResponse() =>
        BadRequest(new BadRequestResponseDto(Notifier.GetNotifications()));

    private IActionResult HttpStatusCodeRespose<T>(object result = null,
                                                   HttpStatusCode statusCode = HttpStatusCode.OK,
                                                   string url = "") where T : class, new()
    {
        switch (statusCode)
        {
            case HttpStatusCode.Created:
            return Created(url, result);

            case HttpStatusCode.OK:
            return Ok(result);
        }

        return StatusCode((int)statusCode);
    }

    protected void NotifyError(string message) =>
        Notifier.Send(new Notification(message));
}
