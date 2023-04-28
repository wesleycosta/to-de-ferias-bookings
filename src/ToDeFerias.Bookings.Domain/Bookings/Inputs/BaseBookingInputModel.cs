namespace ToDeFerias.Bookings.Domain.Bookings.Inputs;

public abstract class BaseBookingInputModel
{
    public DateTimeOffset CheckIn { get; set; }
    public DateTimeOffset CheckOut { get; set; }
    public decimal Value { get; set; }
    public int Adults { get; set; }
    public int Children { get; set; }
}
