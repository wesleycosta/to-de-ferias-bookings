using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;

public sealed class DateOfBirth : IValueObject
{
    public static readonly DateTimeOffset MinValue = new(new DateTime(1900, 01, 01), TimeSpan.Zero);

    public DateTimeOffset Birthday { get; private set; }

    public DateOfBirth(DateTimeOffset birthday)
    {
        if (!IsValid(birthday))
            throw new DomainException("Birthday is invalid");

        Birthday = birthday;
    }

    public static bool IsValid(DateTimeOffset birthday) =>
        birthday > MinValue;

    public override bool Equals(object obj)
    {
        if (obj is not DateOfBirth date)
            return false;

        return Birthday.Date.Equals(date.Birthday);
    }

    public override int GetHashCode() =>
        GetType().GetHashCode() * 907 + Birthday.GetHashCode();

    public override string ToString() =>
        Birthday.ToString();
}
