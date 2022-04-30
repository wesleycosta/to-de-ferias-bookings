using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Domain.Entities.HouseGuestAggregate;
using ToDeFerias.Bookings.Domain.Repositories;
using ToDeFerias.Bookings.Infrastructure.Context;

namespace ToDeFerias.Bookings.Infrastructure.Repositories;

internal sealed class HouseGuestRepository : RepositoryBase<HouseGuest>, IHouseGuestRepository
{
    public HouseGuestRepository(BookingContext context,
                                IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}
