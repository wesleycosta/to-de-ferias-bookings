using ToDeFerias.Bookings.Domain.Aggregates.BookingAggregate;

namespace ToDeFerias.Bookings.Domain.Tests.Builders;

internal abstract class BaseBuilderWithAutoFixture<TObject, TBuilder> : BaseBuilder<TObject, TBuilder> where TBuilder : class, new()
{
    protected readonly Fixture Fixture;

    public BaseBuilderWithAutoFixture() =>
        Fixture = new Fixture();
}
