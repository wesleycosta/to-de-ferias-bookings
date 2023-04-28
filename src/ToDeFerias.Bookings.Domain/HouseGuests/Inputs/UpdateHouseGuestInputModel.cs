namespace ToDeFerias.Bookings.Domain.HouseGuests.Inputs;

public class UpdateHouseGuestInputModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
}
