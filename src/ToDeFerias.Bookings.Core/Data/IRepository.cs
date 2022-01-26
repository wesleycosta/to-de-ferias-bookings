using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Core.Data;

public interface IRepository<in T> : IDisposable where T : Entity, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}
