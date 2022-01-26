namespace ToDeFerias.Bookings.Api.DTOs;

public sealed class BookingDto
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public byte Adults { get; set; }
    public byte Children { get; set; }
}
