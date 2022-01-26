namespace ToDeFerias.Bookings.Domain.Inputs.Bookings;

public sealed class RegisterBookingInputModel
{
    public Guid HouseGuestId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal Value { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
}
