namespace ToDeFerias.Bookings.Domain.Tests.Builders;

internal abstract class BaseBuilder<TObject, TBuilder> where TBuilder : class, new()
{
    protected TObject Object;

    public abstract TBuilder BuildDefault();

    public TObject Create()
    {
        if (Object is null)
            throw new NullReferenceException(nameof(Object));

        return Object;
    }
}
