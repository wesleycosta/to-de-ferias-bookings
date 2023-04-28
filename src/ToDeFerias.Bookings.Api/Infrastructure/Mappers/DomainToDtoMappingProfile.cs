using AutoMapper;
using ToDeFerias.Bookings.Api.Dtos;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Bookings.Aggregates;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;

namespace ToDeFerias.Bookings.Api.Infrastructure.Mappers;

public sealed class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<CommandHandlerResult, CommandHandlerResultDto>();

        CreateMap<Booking, BookingFullDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CheckIn, opt => opt.MapFrom(src => src.Period.CheckIn))
            .ForMember(dest => dest.CheckOut, opt => opt.MapFrom(src => src.Period.CheckOut));

        CreateMap<Booking, BookingBasicInfoDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.CheckIn, opt => opt.MapFrom(src => src.Period.CheckIn))
            .ForMember(dest => dest.CheckOut, opt => opt.MapFrom(src => src.Period.CheckOut));

        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name));

        CreateMap<HouseGuest, HouseGuestFullDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Birthday))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Number));

        CreateMap<HouseGuest, HouseGuestBasicInfoDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.Cpf.Number));
    }
}
