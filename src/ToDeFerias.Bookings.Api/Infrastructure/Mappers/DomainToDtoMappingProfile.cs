using AutoMapper;
using ToDeFerias.Bookings.Api.DTOs;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Api.Infrastructure.Mappers;

public sealed class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<CommandHandlerResult, CommandHandlerResultDto>();

        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.CheckIn,
                       opt => opt.MapFrom(src => src.DateRange.CheckIn))
            .ForMember(dest => dest.CheckOut,
                       opt => opt.MapFrom(src => src.DateRange.CheckOut));
        
        CreateMap<HouseGuest, HouseGuestDto>()
            .ForMember(dest => dest.Email,
                       opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Cpf,
                       opt => opt.MapFrom(src => src.Cpf.Number));

        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.Type,
                       opt => opt.MapFrom(src => src.Type.Name));
    }
}
