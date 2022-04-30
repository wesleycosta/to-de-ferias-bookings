namespace ToDeFerias.Bookings.Api.DTOs;

public sealed class BookingDto : BaseDto
{
    public Guid HouseGuestId { get; set; }
    public Guid RoomId { get; set; }
    public decimal Value { get; set; }
    public byte Adults { get; set; }
    public byte Children { get; set; }
}
