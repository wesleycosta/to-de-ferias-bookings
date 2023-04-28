namespace ToDeFerias.Bookings.Api.Dtos;

public sealed class BookingBasicInfoDto
{
    public Guid Id { get; set; }
    public string Status { get; set; }
    public DateTimeOffset CheckIn { get; set; }
    public DateTimeOffset CheckOut { get; set; }
    public decimal Value { get; set; }
    public byte Adults { get; set; }
    public byte Children { get; set; }
}
