using System.Globalization;
using System.Xml.Linq;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;
using ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.HouseGuestAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Aggregates.BookingAggregate;

public sealed class BookingTests
{
    [Fact]
    public void Constructor_Should_CreateInstance_When_ReceiveParamsInConstructor()
    {
        // arrange
        var booking = new BookingBuilder().BuildDefault()
                                          .Create();

        var checkIn = new DateTimeOffset(new DateTime(2022, 01, 01), TimeSpan.Zero);
        var checkOut = new DateTimeOffset(new DateTime(2022, 01, 02), TimeSpan.Zero);

        // act
        var sut = new Booking(booking.HouseGuestId,
                              booking.RoomId,
                              checkIn,
                              checkOut,
                              booking.Value,
                              booking.Adults,
                              booking.Children);

        // assert
        sut.HouseGuestId.Should().Be(booking.HouseGuestId);
        sut.RoomId.Should().Be(booking.RoomId);
        sut.Period.CheckIn.Should().Be(checkIn);
        sut.Period.CheckOut.Should().Be(checkOut);
        sut.Value.Should().Be(booking.Value);
        sut.Adults.Should().Be(booking.Adults);
        sut.Children.Should().Be(booking.Children);
    }

    [Fact]
    public void Update_Should_UpdateBookingProperties_When_ItIsInvoked()
    {
        // arrange
        var sut = new BookingBuilder().BuildDefault()
                                      .Create();

        var checkIn = new DateTime(2022, 01, 01);
        var checkOut = new DateTime(2022, 01, 02);

        // act
        sut.Update(value: 100,
                   adults: 2,
                   children: 1,
                   checkIn,
                   checkOut);

        // assert
        sut.Value.Should().Be(100);
        sut.Adults.Should().Be(2);
        sut.Children.Should().Be(1);
        sut.Period.CheckIn.Should().Be(checkIn);
        sut.Period.CheckOut.Should().Be(checkOut);
    }

    [Fact]
    public void SetHouseGuest_Should_SetHouseGuestProperty_When_ItIsInvoked()
    {
        // arrange
        var sut = new Booking();
        var houseGuest = new HouseGuestBuilder().BuildDefault()
                                                .Create();

        // act
        sut.SetHouseGuest(houseGuest);

        // assert
        sut.HouseGuest.Should().NotBeNull();
        sut.HouseGuestId.Should().Be(houseGuest.Id);
        sut.HouseGuest.Should().Be(houseGuest);
    }

    [Fact]
    public void SetRoom_Should_SetRoomProperty_When_ItIsInvoked()
    {
        // arrange
        var sut = new Booking();
        var room = new RoomBuilder().BuildDefault()
                                    .Create();

        // act
        sut.SetRoom(room);

        // assert
        sut.Room.Should().NotBeNull();
        sut.RoomId.Should().Be(room.Id);
        sut.Room.Should().Be(room);
    }

    [Theory]
    [MemberData(nameof(GetTestScenariosForHasDateChanged))]
    public void HasDateChanged_Should_ReturnTrue_When_PeriodChanges(DateTimeOffset checkInChanged, 
                                                                    DateTimeOffset checkOutChanged)
    {
        // arrange
        var checkIn = new DateTimeOffset(new DateTime(2022, 01, 01), TimeSpan.Zero);
        var checkOut = new DateTimeOffset(new DateTime(2022, 01, 02), TimeSpan.Zero);

        var sut = new Booking(Guid.NewGuid(),
                              Guid.NewGuid(),
                              checkIn,
                              checkOut,
                              value: 100,
                              adults: 2,
                              children: 1);

        var periodChanged = new DateRangeBooking(checkInChanged,
                                                 checkOutChanged);

        // act
        var result = sut.HasDateChanged(periodChanged);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void HasDateChanged_Should_ReturnFalse_When_PeriodHasNotChanged()
    {
        // arrange
        var checkIn = new DateTimeOffset(new DateTime(2022, 01, 01), TimeSpan.Zero);
        var checkOut = new DateTimeOffset(new DateTime(2022, 01, 02), TimeSpan.Zero);

        var sut = new Booking(Guid.NewGuid(),
                              Guid.NewGuid(),
                              checkIn,
                              checkOut,
                              value: 100,
                              adults: 2,
                              children: 1);

        var periodChanged = new DateRangeBooking(checkIn,
                                                 checkOut);

        // act
        var result = sut.HasDateChanged(periodChanged);

        // assert
        result.Should().BeFalse();
    }

    public static IEnumerable<object[]> GetTestScenariosForHasDateChanged()
    {
        yield return new object[]
        {
            new DateTimeOffset(new DateTime(2022, 01, 01), TimeSpan.Zero),
            new DateTimeOffset(new DateTime(2022, 01, 03), TimeSpan.Zero),
        };

        yield return new object[]
        {
            new DateTimeOffset(new DateTime(2021, 12, 31), TimeSpan.Zero),
            new DateTimeOffset(new DateTime(2022, 01, 02), TimeSpan.Zero),
        };

        yield return new object[]
        {
            new DateTimeOffset(new DateTime(2021, 12, 31), TimeSpan.Zero),
            new DateTimeOffset(new DateTime(2022, 01, 03), TimeSpan.Zero),
        };
    }
}
