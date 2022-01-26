using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

public sealed class DateRangeBooking : IValueObject
{
    public static readonly DateTime MinValue = new(1900, 1, 1);

    public DateTime CheckIn { get; private set; }
    public DateTime CheckOut { get; private set; }

    public DateRangeBooking(DateTime checkIn, DateTime checkOut)
    {
        if (!IsValidDate(checkIn))
            throw new DomainException("CheckInDate is invalid");

        if (!IsValidDate(checkOut))
            throw new DomainException("CheckOutDate is invalid");

        CheckIn = checkIn;
        CheckOut = checkOut;
    }

    public static bool IsValid(DateTime checkIn, DateTime checkOut) =>
        IsValidDate(checkIn) && IsValidDate(checkOut);

    private static bool IsValidDate(DateTime date) =>
        date > MinValue;

    public override bool Equals(object? obj)
    {
        if (obj is not DateRangeBooking date)
            return false;

        return CheckIn.Date.Equals(date.CheckIn.Date) && CheckOut.Date.Equals(date.CheckOut.Date);
    }

    public override string ToString() =>
        $"{CheckIn.Date} of {CheckOut.Date}";

    public override int GetHashCode() =>
        (GetType().GetHashCode() * 907) + CheckIn.GetHashCode() + CheckOut.GetHashCode();
}
