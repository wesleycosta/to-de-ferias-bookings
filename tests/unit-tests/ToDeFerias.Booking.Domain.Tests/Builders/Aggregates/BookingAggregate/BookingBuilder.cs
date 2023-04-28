using ToDeFerias.Bookings.Domain.Bookings.Aggregates;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.BookingAggregate;

internal sealed class BookingBuilder : BaseBuilderWithAutoFixture<Booking, BookingBuilder>
{
    public override BookingBuilder BuildDefault()
    {
        Object = Fixture.Create<Booking>();

        return this;
    }
}
