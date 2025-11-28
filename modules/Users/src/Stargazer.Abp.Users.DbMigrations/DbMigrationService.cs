using Stargazer.Abp.Users.EntityFrameworkCore.DbMigrations;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Stargazer.Abp.Users.DbMigrations
{
    public class DbMigrationService(IDataSeeder dataSeeder) : ITransientDependency
    {
        public async Task MigrateAsync()
        {
            await dataSeeder.SeedAsync();
        }
    }
}