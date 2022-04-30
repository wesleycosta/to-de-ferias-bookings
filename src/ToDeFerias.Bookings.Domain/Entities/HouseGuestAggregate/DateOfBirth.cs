using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Entities.HouseGuestAggregate;

public sealed class DateOfBirth : IValueObject
{
    public static readonly DateTime MinValue = new(1900, 1, 1);

    public DateTime Birthday { get; private set; }

    public DateOfBirth(DateTime birthday)
    {
        if (!IsValid(birthday))
            throw new DomainException("Birthday is invalid");

        Birthday = birthday;
    }

    public static bool IsValid(DateTime birthday) =>
        birthday > MinValue && birthday <= DateTime.UtcNow.AddYears(-18);

    public override bool Equals(object obj)
    {
        if (obj is not DateOfBirth date)
            return false;

        return Birthday.Date.Equals(date.Birthday);
    }

    public override int GetHashCode() =>
        (GetType().GetHashCode() * 907) + Birthday.GetHashCode();

    public override string ToString() =>
        Birthday.ToString();
}
