namespace ToDeFerias.Bookings.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
