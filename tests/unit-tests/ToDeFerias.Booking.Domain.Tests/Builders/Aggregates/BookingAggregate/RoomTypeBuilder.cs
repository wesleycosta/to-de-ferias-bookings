using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.BookingAggregate;

internal sealed class RoomTypeBuilder : BaseBuilder<RoomType, RoomTypeBuilder>
{
    public override RoomTypeBuilder BuildDefault()
    {
        Object = new RoomType(Guid.Parse("c6c61c57-952f-4a5f-aab7-045bec4cca67"),
                             "Master");

        return this;
    }
}
