using Serilog;
using ToDeFerias.Bookings.Api.Infrastructure;
using ToDeFerias.Bookings.Api.Infrastructure.Api.Configurations;
using ToDeFerias.Bookings.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
       .AddApiConfiguration(builder.Configuration)
       .AddIocConfiguration(builder.Configuration);

var app = builder.Build();
app.UseApiConfiguration();
app.MapControllers();

app.AddStartupAndShutdownLog()
   .ApplyMigrate();

app.Run();
