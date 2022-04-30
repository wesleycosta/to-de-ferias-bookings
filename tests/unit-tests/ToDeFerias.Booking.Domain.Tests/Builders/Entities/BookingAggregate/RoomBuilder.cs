using AutoFixture;
using System;
using ToDeFerias.Bookings.Domain.Entities.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Entities.BookingAggregate;

internal sealed class RoomBuilder : BaseBuilderWithAutoFixture<Room, RoomBuilder>
{
    public override RoomBuilder BuildDefault()
    {
        Object = new Room(id: Guid.NewGuid(),
                          number: Fixture.Create<byte>(),
                          roomTypeId: Guid.NewGuid());

        return this;
    }

    public RoomBuilder WithId(Guid id)
    {
        Object.Id = id;

        return this;
    }
}
