﻿namespace ToDeFerias.Bookings.Api.Dtos;

public sealed class HouseGuestBasicInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
}
