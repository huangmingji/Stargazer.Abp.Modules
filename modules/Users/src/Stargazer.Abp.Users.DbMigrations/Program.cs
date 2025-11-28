using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Stargazer.Abp.Users.DbMigrations;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "logs/logs.txt"))
    .WriteTo.Console()
    .CreateLogger();
        
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

await Host.CreateDefaultBuilder(args)
    .ConfigureLogging((context, logging) => logging.ClearProviders())
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<DbMigratorHostedService>();
    }).RunConsoleAsync();

