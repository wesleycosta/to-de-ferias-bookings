namespace ToDeFerias.Bookings.Api.DTOs;

public sealed class RoomDto
{
    public Guid Id { get; set; }
    public byte Number { get; set; }
    public string Type { get; set; }
}
