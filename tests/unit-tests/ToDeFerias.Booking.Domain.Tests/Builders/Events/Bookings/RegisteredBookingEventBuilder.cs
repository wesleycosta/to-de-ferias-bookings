using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Events.Bookings;
using ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Events.Bookings;

internal sealed class RegisteredBookingEventBuilder : BaseBuilderWithAutoFixture<RegisteredBookingEvent, RegisteredBookingEventBuilder>
{
    public override RegisteredBookingEventBuilder BuildDefault()
    {
        var houseGuest = new HouseGuestBuilder().BuildDefault()
                                                .Create();

        var room = new RoomBuilder().BuildDefault()
                                    .Create();

        var checkIn = new DateTimeOffset(new DateTime(2022, 01, 01), TimeSpan.Zero);
        var checkOut = new DateTimeOffset(new DateTime(2022, 01, 02), TimeSpan.Zero);
        var dateRangeBooking = new DateRangeBooking(checkIn, checkOut);

        Object = new RegisteredBookingEvent(Guid.NewGuid(),
                                            houseGuest,
                                            room,
                                            dateRangeBooking,
                                            Fixture.Create<decimal>(),
                                            Fixture.Create<byte>(),
                                            Fixture.Create<byte>());

        return this;
    }
}
