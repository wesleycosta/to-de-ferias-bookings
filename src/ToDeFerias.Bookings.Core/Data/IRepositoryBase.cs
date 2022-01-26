using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Core.Data;

public interface IRepositoryBase<T> : IRepository<T> where T : Entity, IAggregateRoot
{
    void Add(T entity);
    void Update(T entity);
    Task Remove(Guid id);
    Task<T?> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
}
