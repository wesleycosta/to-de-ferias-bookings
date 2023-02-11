namespace ToDeFerias.Bookings.Api.Dtos.Booking;

public sealed class BookingDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTimeOffset CheckIn { get; set; }
    public DateTimeOffset CheckOut { get; set; }
    public decimal Value { get; set; }
    public byte Adults { get; set; }
    public byte Children { get; set; }
}
