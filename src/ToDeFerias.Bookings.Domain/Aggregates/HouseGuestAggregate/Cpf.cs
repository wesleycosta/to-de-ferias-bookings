using NetDevPack.Brasil.Documentos.Validacao;
using NetDevPack.Utilities;
using ToDeFerias.Bookings.Core.DomainObjects;

namespace ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

public sealed class Cpf : IValueObject
{
    public static readonly byte Length = 11;
    public string Number { get; private set; }

    public Cpf(string number)
    {
        if (!IsValid(number))
            throw new DomainException("CPF is invalid");

        Number = number.OnlyNumbers();
    }

    public static bool IsValid(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return false;

        return new CpfValidador(cpf).EstaValido();
    }

    public override bool Equals(object obj)
    {
        if (obj is not Cpf email)
            return false;

        return email.Number.Equals(Number);
    }

    public override int GetHashCode() =>
      (GetType().GetHashCode() * 907) + Number.GetHashCode();

    public override string ToString()
    {
        const string pattern = @"{0:000\.000\.000\-00}";
        return string.Format(pattern, Convert.ToUInt64(Number));
    }
}
