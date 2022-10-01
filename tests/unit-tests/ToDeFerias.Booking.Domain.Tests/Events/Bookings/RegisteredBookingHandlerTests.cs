using Microsoft.Extensions.Options;
using ToDeFerias.Bookings.Core.Bus;
using ToDeFerias.Bookings.Core.Logger;
using ToDeFerias.Bookings.Core.Messages;
using ToDeFerias.Bookings.Core.Settings;
using ToDeFerias.Bookings.Domain.Events.Bookings;
using ToDeFerias.Bookings.Domain.Tests.Builders.Core.Settings;
using ToDeFerias.Bookings.Domain.Tests.Builders.Events.Bookings;

namespace ToDeFerias.Bookings.Domain.Tests.Events.Bookings;

public sealed class RegisteredBookingHandlerTests
{
    private readonly RegisteredBookingHandler _sut;
    private readonly Mock<ITrace> _trace;
    private readonly Mock<IMessageBus> _messageBus;
    private readonly Mock<IOptions<BusSettings>> _messageBusSettings;

    public RegisteredBookingHandlerTests()
    {
        _trace = new Mock<ITrace>();
        _messageBus = new Mock<IMessageBus>();
        _messageBusSettings = new Mock<IOptions<BusSettings>>();

        var messageBusSettings = new BusSettingsBuilder().BuildDefault()
                                                         .Create();

        _messageBusSettings.Setup(p => p.Value)
                           .Returns(messageBusSettings);

        _sut = new RegisteredBookingHandler(_trace.Object,
                                            _messageBus.Object,
                                            _messageBusSettings.Object);
    }

    [Fact]
    public async Task Handle_Should_PublishEventToTheNotificationQueue_When_RegisteredBooking()
    {
        // arrange
        var @event = new RegisteredBookingEventBuilder().BuildDefault()
                                                        .Create();

        // act
        await _sut.Handle(@event, default);

        // assert
        _messageBus.Verify(p => p.Publish(It.Is<EventWrapper>(q => q.Type.Equals("RegisteredBookingEvent")),
                                          It.Is<QueueSettings>(q => q.Queue.Equals("notifications-events") &&
                                                                    q.Exchange.Equals("notifications-exchange") &&
                                                                    q.RoutingKey.Equals("notification-routing-key"))),
                                Times.Once);

        _messageBus.VerifyNoOtherCalls();
    }
}
