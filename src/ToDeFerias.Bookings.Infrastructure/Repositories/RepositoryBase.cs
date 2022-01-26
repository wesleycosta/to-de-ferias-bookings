using Microsoft.EntityFrameworkCore;
using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Core.DomainObjects;
using ToDeFerias.Bookings.Infrastructure.Context;

namespace ToDeFerias.Bookings.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : Entity, IAggregateRoot
{
    protected readonly BookingContext Context;
    protected readonly DbSet<T> DbSet;

    protected RepositoryBase(BookingContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public IUnitOfWork UnitOfWork =>
        throw new NotImplementedException();

    public void Add(T entity) =>
        DbSet.Add(entity);

    public void Update(T entity) =>
        DbSet.Update(entity);

    public async Task Remove(Guid id)
    {
        var entity = await GetById(id);
        if (entity is null)
            return;

        DbSet.Remove(entity);
    }

    public async Task<T?> GetById(Guid id) =>
        await DbSet.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<T>> GetAll() =>
        await DbSet.ToListAsync();

    public void Dispose() =>
        Context?.Dispose();
}
