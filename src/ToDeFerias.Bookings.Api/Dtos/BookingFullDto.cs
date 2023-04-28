﻿namespace ToDeFerias.Bookings.Api.Dtos;

public sealed class BookingFullDto
{
    public Guid Id { get; set; }
    public HouseGuestBasicInfoDto HouseGuest { get; set; }
    public RoomDto Room { get; set; }
    public string Status { get; set; }
    public DateTimeOffset CheckIn { get; set; }
    public DateTimeOffset CheckOut { get; set; }
    public decimal Value { get; set; }
    public byte Adults { get; set; }
    public byte Children { get; set; }
}
