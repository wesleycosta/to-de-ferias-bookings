using ToDeFerias.Bookings.Domain.HouseGuests.Aggregates;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.HouseGuestAggregate;

internal sealed class HouseGuestBuilder : BaseBuilderWithAutoFixture<HouseGuest, HouseGuestBuilder>
{
    private readonly string _emailAddress = "username@mail.com";
    private readonly string _cpf = "648.790.800-20";

    public override HouseGuestBuilder BuildDefault()
    {
        Object = new HouseGuest("Superman",
            _emailAddress,
            _cpf,
            DateTimeOffset.Now.AddYears(-30));

        return this;
    }

    public HouseGuestBuilder WithId(Guid id)
    {
        Object = new HouseGuest(Fixture.Create<string>(),
            _emailAddress,
            _cpf,
            DateTimeOffset.Now.AddYears(-30));

        Object.SetId(id);

        return this;
    }
}
