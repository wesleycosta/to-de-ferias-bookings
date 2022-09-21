using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;
using ToDeFerias.Bookings.Infrastructure.Context;
using ToDeFerias.Bookings.Infrastructure.Repositories;

namespace ToDeFerias.Bookings.Infrastructure.Data.Repositories;

public sealed class HouseGuestRepository : RepositoryBase<HouseGuest>, IHouseGuestRepository
{
    public HouseGuestRepository(BookingsContext context,
                                IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}
