using System.Text.RegularExpressions;
using ToDeFerias.Bookings.Core.DomainObjects;
using DomainException = ToDeFerias.Bookings.Core.DomainObjects.DomainException;

namespace ToDeFerias.Bookings.Domain.Entities.HouseGuestAggregate;

public sealed class Email : IValueObject
{
    public static readonly byte MinLength = 5;
    public static readonly byte MaxLength = 254;
    public string Address { get; private set; }

    protected Email() { }

    public Email(string emailAddress)
    {
        if (!IsValid(emailAddress))
            throw new DomainException("E-mail is invalid");

        Address = emailAddress;
    }

    public static bool IsValid(string email)
    {
        if (string.IsNullOrEmpty(email) || email.Length < MinLength)
            return false;

        var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return regexEmail.IsMatch(email);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Email email)
            return false;

        return email.Address.Equals(Address);
    }

    public override int GetHashCode() =>
        (GetType().GetHashCode() * 907) + Address.GetHashCode();

    public override string ToString() =>
        Address;
}
