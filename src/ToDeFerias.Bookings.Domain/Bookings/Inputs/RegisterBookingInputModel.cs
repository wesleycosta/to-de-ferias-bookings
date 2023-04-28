namespace ToDeFerias.Bookings.Domain.Bookings.Inputs;

public sealed class RegisterBookingInputModel : BaseBookingInputModel
{
    public Guid HouseGuestId { get; set; }
    public Guid RoomId { get; set; }
}
