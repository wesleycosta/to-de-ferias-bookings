using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.HouseGuestAggregate;

internal sealed class HouseGuestBuilder : BaseBuilderWithAutoFixture<HouseGuest, HouseGuestBuilder>
{
    private readonly string _emailAddress = "username@mail.com";
    private readonly string _cpf = "648.790.800-20";

    public override HouseGuestBuilder BuildDefault()
    {
        Object = new HouseGuest(id: Guid.NewGuid(),
                                name: "Superman",
                                _emailAddress,
                                _cpf);

        return this;
    }

    public HouseGuestBuilder WithId(Guid id)
    {
        Object = new HouseGuest(id: id,
                               name: Fixture.Create<string>(),
                               _emailAddress,
                               _cpf);

        return this;
    }
}
