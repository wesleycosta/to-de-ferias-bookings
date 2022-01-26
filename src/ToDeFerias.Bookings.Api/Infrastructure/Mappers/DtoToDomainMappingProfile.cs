using AutoMapper;
using ToDeFerias.Bookings.Api.DTOs;
using ToDeFerias.Bookings.Domain.Inputs.Bookings;

namespace ToDeFerias.Bookings.Api.Infrastructure.Mappers;

public sealed class DtoToDomainMappingProfile : Profile
{
    public DtoToDomainMappingProfile() =>
        CreateMap<RegisterBookingDto, RegisterBookingInputModel>();
}
