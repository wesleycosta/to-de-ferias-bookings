namespace ToDeFerias.Bookings.Domain.Inputs.Bookings;

public sealed class RegisterBookingInputModel : BaseBookingInputModel
{
    public Guid HouseGuestId { get; set; }
    public Guid RoomId { get; set; }
}
