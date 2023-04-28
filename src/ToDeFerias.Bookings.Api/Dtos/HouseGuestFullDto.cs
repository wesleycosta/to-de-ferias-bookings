namespace ToDeFerias.Bookings.Api.Dtos;

public class HouseGuestFullDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
}
