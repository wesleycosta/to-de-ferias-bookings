using Microsoft.EntityFrameworkCore;
using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;
using ToDeFerias.Bookings.Infrastructure.Context;
using ToDeFerias.Bookings.Infrastructure.Repositories;

namespace ToDeFerias.Bookings.Infrastructure.Data.Repositories;

public sealed class HouseGuestRepository : RepositoryBase<HouseGuest>, IHouseGuestRepository
{
    public HouseGuestRepository(BookingsContext context,
                                IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<HouseGuest> GetByCpf(string cpf)
        => await DbSet.FirstOrDefaultAsync(p => p.Cpf.Number.Equals(cpf));
    public async Task<HouseGuest> GetByEmail(string email)
        => await DbSet.FirstOrDefaultAsync(p => p.Email.Address.Equals(email));

    public async Task<HouseGuest> GetByCpfAndIdIsDifferentFrom(string cpf, Guid aggregateId)
        => await DbSet.FirstOrDefaultAsync(p => p.Cpf.Number.Equals(cpf) && p.Id != aggregateId);

    public async Task<HouseGuest> GetByEmailAndIdIsDifferentFrom(string email, Guid aggregateId)
        => await DbSet.FirstOrDefaultAsync(p => p.Email.Address.Equals(email) && p.Id != aggregateId);
}
