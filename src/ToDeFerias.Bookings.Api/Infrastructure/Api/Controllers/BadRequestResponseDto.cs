using ToDeFerias.Bookings.Api.Infrastructure.Notifications;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Controllers;

public sealed class BadRequestResponseDto
{
    public IEnumerable<string> Errors { get; }

    public BadRequestResponseDto(IReadOnlyList<Notification> notifications) =>
        Errors = notifications.Select(p => p?.Message);
}
