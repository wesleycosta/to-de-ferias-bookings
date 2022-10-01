using ToDeFerias.Bookings.Core.Data;
using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Aggregates.HouseGuestAggregate;
using ToDeFerias.Bookings.Domain.Commands.Bookings;
using ToDeFerias.Bookings.Domain.Events.Bookings;
using ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.BookingAggregate;
using ToDeFerias.Bookings.Domain.Tests.Builders.Aggregates.HouseGuestAggregate;
using ToDeFerias.Bookings.Domain.Tests.Builders.Commands;

namespace ToDeFerias.Bookings.Domain.Tests.Commands.Bookings;

public sealed class RegisterBookingCommandHandleTests
{
    private readonly RegisterBookingCommandHandler _sut;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IBookingRepository> _bookingRepository;
    private readonly Mock<IHouseGuestRepository> _houseGuestRepository;

    public RegisterBookingCommandHandleTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _bookingRepository = new Mock<IBookingRepository>();
        _houseGuestRepository = new Mock<IHouseGuestRepository>();

        _sut = new RegisterBookingCommandHandler(_bookingRepository.Object,
                                                 _houseGuestRepository.Object);
    }

    [Fact]
    public async Task Handle_Should_RegisterBooking_When_RoomIsAvailableAndValidData()
    {
        // arrange
        var houseGuestId = Guid.NewGuid();
        var roomId = Guid.NewGuid();
        var checkIn = DateTimeOffset.UtcNow;
        var checkOut = DateTimeOffset.UtcNow;
        var value = 100.33m;
        byte adults = 2;
        byte children = 1;

        var inputModel = new RegisterBookingInputModelBuilder().BuildDefault()
                                                               .WithHouseGuestId(houseGuestId)
                                                               .WithRoomId(roomId)
                                                               .WithCheckIn(checkIn)
                                                               .WithCheckOut(checkOut)
                                                               .WithValue(value)
                                                               .WithAdults(adults)
                                                               .WithChildren(children)
                                                               .Create();

        var command = new RegisterBookingCommand(inputModel);

        SetupUnitOfWork();
        SetupBookingRepository(roomId);
        SetupHouseGuestRepository(houseGuestId);

        // act
        var commandHandlerResult = await _sut.Handle(command, default);

        // assert
        commandHandlerResult.Should().NotBeNull();
        commandHandlerResult.IsValid.Should().BeTrue();

        var booking = commandHandlerResult.Response as Booking;
        booking.Should().NotBeNull();
        booking.Id.Should().NotBeEmpty();
        booking.HouseGuestId.Should().Be(houseGuestId);
        booking.RoomId.Should().Be(roomId);
        booking.Period.Should().Be(new DateRangeBooking(checkIn, checkOut));
        booking.Value.Should().Be(value);
        booking.Adults.Should().Be(adults);
        booking.Children.Should().Be(children);
        booking.Status.Should().Be(BookingStatus.Booked);

        booking.DomainEvents.Should().HaveCount(1);
        booking.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(RegisteredBookingEvent));

        _bookingRepository.Verify(p => p.ItsBooked(roomId, It.IsAny<DateRangeBooking>()), Times.Once);
        _bookingRepository.Verify(p => p.GetRoomById(roomId), Times.Once);
        _bookingRepository.Verify(p => p.Add(It.IsAny<Booking>()), Times.Once);
        _houseGuestRepository.Verify(p => p.GetById(houseGuestId), Times.Once);
        _unitOfWork.Verify(p => p.Commit(), Times.Once);

        _bookingRepository.VerifyNoOtherCalls();
        _houseGuestRepository.VerifyNoOtherCalls();
        _unitOfWork.VerifyNoOtherCalls();
    }

    private void SetupUnitOfWork() =>
        _unitOfWork.Setup(p => p.Commit())
                   .ReturnsAsync(true);

    private void SetupBookingRepository(Guid roomId)
    {
        var room = new RoomBuilder().BuildDefault()
                                    .WithId(roomId)
                                    .Create();

        _bookingRepository.Setup(p => p.ItsBooked(roomId, It.IsAny<DateRangeBooking>()))
                           .ReturnsAsync(false);

        _bookingRepository.Setup(p => p.GetRoomById(roomId))
                          .ReturnsAsync(room);

        _bookingRepository.Setup(p => p.UnitOfWork)
                          .Returns(_unitOfWork.Object);
    }

    private void SetupHouseGuestRepository(Guid houseGuestId)
    {
        var houseGuest = new HouseGuestBuilder().BuildDefault()
                                                .WithId(houseGuestId)
                                                .Create();

        _houseGuestRepository.Setup(p => p.GetById(houseGuestId))
                             .ReturnsAsync(houseGuest);
    }
}
