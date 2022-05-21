namespace ToDeFerias.Bookings.Api.DTOs;

public sealed class BookingDto
{
    public Guid Id { get; set; }
    public HouseGuestDto HouseGuest { get; set; }
    public RoomDto Room { get; set; }
    public string Status { get; set; }
    public DateTimeOffset CheckIn { get; set; }
    public DateTimeOffset CheckOut { get; set; }
    public decimal Value { get; set; }
    public byte Adults { get; set; }
    public byte Children { get; set; }
}
