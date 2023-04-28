namespace ToDeFerias.Bookings.Api.Dtos;

public sealed class RoomDto
{
    public Guid Id { get; set; }
    public byte Number { get; set; }
    public string Type { get; set; }
}
