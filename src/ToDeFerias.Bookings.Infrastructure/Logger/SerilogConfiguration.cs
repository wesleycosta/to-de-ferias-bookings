using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace ToDeFerias.Bookings.Infrastructure.Logger;

public static class SerilogConfiguration
{
    public static IServiceCollection AddSerilog(this IServiceCollection services,
                                                  IConfiguration configuration)
    {
        var elasticsearchUri = new Uri(configuration["Elasticsearch:Uri"]);
        var options = new ElasticsearchSinkOptions(elasticsearchUri)
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            IndexFormat = configuration["Elasticsearch:IndexFormat"]
        };

        Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Elasticsearch(options)
                    .WriteTo.Console()
                    .CreateLogger();

        return services.AddSingleton(Log.Logger);
    }
}
