using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations.Settings;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private static readonly string _apiKeyName = "api-key";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(_apiKeyName, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey not found"
            };

            return;
        }

        var services = context.HttpContext.RequestServices;
        var setting = services.GetService<IOptions<ApiKeySetting>>();

        if (!extractedApiKey.Equals(setting?.Value.Secret))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Access Forbidden"
            };

            return;
        }

        await next();
    }
}
