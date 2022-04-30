using AutoMapper;
using ToDeFerias.Bookings.Api.DTOs;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;
using ToDeFerias.Bookings.Domain.Entities.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Api.Infrastructure.Mappers;

public sealed class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Booking, BookingDto>();
        CreateMap<HouseGuest, HouseGuestDto>();
        CreateMap<Room, RoomDto>();
        CreateMap<CommandHandlerResult, CommandHandlerResultDto>();
    }
}
