using AutoFixture;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.BookingAggregate;

internal sealed class RoomBuilder : BaseBuilderWithAutoFixture<Room, RoomBuilder>
{
    public override RoomBuilder BuildDefault()
    {
        var roomType = new RoomTypeBuilder().BuildDefault()
                                            .Create();

        Object = new Room(id: Guid.NewGuid(),
                          number: Fixture.Create<byte>(),
                          roomTypeId: roomType.Id);

        Object.SetType(roomType);

        return this;
    }

    public RoomBuilder WithId(Guid id)
    {
        Object.Id = id;

        return this;
    }
}
