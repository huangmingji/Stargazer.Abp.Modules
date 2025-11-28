using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;

namespace Stargazer.Abp.Users.DbMigrations
{
    public class DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var application = await AbpApplicationFactory.CreateAsync<DbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });
            await application.InitializeAsync();
            await application
                .ServiceProvider
                .GetRequiredService<DbMigrationService>()
                .MigrateAsync();

            await application.ShutdownAsync();

            hostApplicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    }
}