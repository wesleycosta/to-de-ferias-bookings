namespace ToDeFerias.Bookings.Core.DomainObjects;

public sealed class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
