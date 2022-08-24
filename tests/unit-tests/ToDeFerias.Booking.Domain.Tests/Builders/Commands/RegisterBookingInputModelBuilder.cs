using ToDeFerias.Bookings.Domain.Inputs.Bookings;

namespace ToDeFerias.Bookings.Domain.Tests.Builders.Commands;

internal sealed class RegisterBookingInputModelBuilder : BaseBuilderWithAutoFixture<RegisterBookingInputModel, RegisterBookingInputModelBuilder>
{
    public override RegisterBookingInputModelBuilder BuildDefault()
    {
        Object = Fixture.Create<RegisterBookingInputModel>();

        return this;
    }

    public RegisterBookingInputModelBuilder WithHouseGuestId(Guid houseGuestId)
    {
        Object.HouseGuestId = houseGuestId;

        return this;
    }

    public RegisterBookingInputModelBuilder WithRoomId(Guid roomId)
    {
        Object.RoomId = roomId;

        return this;
    }

    public RegisterBookingInputModelBuilder WithCheckIn(DateTimeOffset checkIn)
    {
        Object.CheckIn = checkIn;

        return this;
    }

    public RegisterBookingInputModelBuilder WithCheckOut(DateTimeOffset checkOut)
    {
        Object.CheckOut = checkOut;

        return this;
    }

    public RegisterBookingInputModelBuilder WithValue(decimal value)
    {
        Object.Value = value;

        return this;
    }

    public RegisterBookingInputModelBuilder WithAdults(byte adults)
    {
        Object.Adults = adults;

        return this;
    }

    public RegisterBookingInputModelBuilder WithChildren(byte children)
    {
        Object.Children = children;

        return this;
    }
}
