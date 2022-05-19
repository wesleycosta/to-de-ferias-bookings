using AutoFixture;
using System;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.HouseGuestAggregate;

internal sealed class HouseGuestBuilder : BaseBuilderWithAutoFixture<HouseGuest, HouseGuestBuilder>
{
    public override HouseGuestBuilder BuildDefault()
    {
        Object = new HouseGuest(id: Guid.NewGuid(),
                                name: Fixture.Create<string>(),
                                emailAddress: "username@mail.com");

        return this;
    }
}
