namespace ToDeFerias.Bookings.Api.Dtos.Booking;

public sealed class HouseGuestDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
}
