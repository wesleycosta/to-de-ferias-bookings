using ToDeFerias.Bookings.Core.Data;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;

public interface IHouseGuestRepository : IRepositoryBase<HouseGuest>
{
    Task<HouseGuest> GetByCpf(string cpf);
    Task<HouseGuest> GetByEmail(string email);
    Task<HouseGuest> GetByCpfAndIdIsDifferentFrom(string cpf, Guid aggregateId);
    Task<HouseGuest> GetByEmailAndIdIsDifferentFrom(string email, Guid aggregateId);
}
