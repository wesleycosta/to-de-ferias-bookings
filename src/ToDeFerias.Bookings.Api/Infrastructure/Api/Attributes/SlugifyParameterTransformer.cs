using System.Text.RegularExpressions;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;

internal sealed class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    string? IOutboundParameterTransformer.TransformOutbound(object? value) =>
        Regex.Replace(value?.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLower();
}
