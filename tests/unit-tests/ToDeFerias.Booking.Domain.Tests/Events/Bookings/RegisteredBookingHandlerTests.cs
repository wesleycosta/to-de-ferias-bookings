using Microsoft.Extensions.Options;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Settings;
using ToDeFerias.Bookings.Domain.Events.Bookings;
using ToDeFerias.Bookings.Domain.Tests.Builders.Core.Settings;
using ToDeFerias.Bookings.Domain.Tests.Builders.Events.Bookings;

namespace ToDeFerias.Bookings.Domain.Tests.Events.Bookings;

public sealed class RegisteredBookingHandlerTests
{
    private readonly RegisteredBookingHandler _sut;
    private readonly Mock<IMessageBus> _messageBus;
    private readonly Mock<IOptions<BusSettings>> _messageBusSettings;

    public RegisteredBookingHandlerTests()
    {
        _messageBus = new Mock<IMessageBus>();
        _messageBusSettings = new Mock<IOptions<BusSettings>>();

        var messageBusSettings = new MessageBusSettingsBuilder().BuildDefault()
                                                                .Create();

        _messageBusSettings.Setup(p => p.Value)
                           .Returns(messageBusSettings);

        _sut = new RegisteredBookingHandler(_messageBus.Object,
                                            _messageBusSettings.Object);
    }

    [Fact]
    public async Task Handle_Should_PublishEventToTheNotificationQueue_When_RegisteredBooking()
    {
        // arrange
        var @event = new RegisteredBookingEventBuilder().BuildDefault()
                                                        .Create();

        var subject = "New Booking";
        var addressee = @event.HouseGuestEmail.Address;
        var message = "Thank you Superman;\r\nNew reservation made for room Master at time period 01/01/2022 00:00:00 of 02/01/2022 00:00:00.";

        // act
        await _sut.Handle(@event, default);

        // assert
        _messageBus.Verify(p => p.Publish(It.Is<NotificationEvent>(q =>
                                    q.Subject.Equals(subject) &&
                                    q.Addressee.Equals(addressee) &&
                                    q.Message.Equals(message)),
                                _messageBusSettings.Object.Value.NotificationsQueueName),
                                Times.Once);

        _messageBus.VerifyNoOtherCalls();
    }
}
