using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Attributes;

namespace ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations;

internal static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ToDeFerias Booking API",
                Version = "v1",
            });

            options.OperationFilter<CustomHeaderSwaggerAttribute>();
            options.SchemaFilter<EnumSchemaFilter>();
        });

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app) =>
        app.UseSwagger(c => c.RouteTemplate = "api-docs/{documentName}/swagger.json")
        .UseSwaggerUI(c =>
        {
            c.RoutePrefix = "api-docs";
            c.SwaggerEndpoint("/api-docs/v1/swagger.json", "ToDeFerias Booking API");
            c.DocExpansion(DocExpansion.List);
        });
}
