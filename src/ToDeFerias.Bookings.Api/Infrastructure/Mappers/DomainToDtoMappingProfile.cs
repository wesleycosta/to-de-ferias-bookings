using AutoMapper;
using ToDeFerias.Bookings.Api.DTOs;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

namespace ToDeFerias.Bookings.Api.Infrastructure.Mappers;

public sealed class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Booking, BookingDto>();
        CreateMap<CommandHandlerResult, CommandHandlerResultDto>();
    }
}
