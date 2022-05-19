using AutoFixture;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.BookingAggregate;

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
