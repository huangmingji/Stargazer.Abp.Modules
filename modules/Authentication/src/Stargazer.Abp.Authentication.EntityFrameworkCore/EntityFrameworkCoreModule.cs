using Microsoft.Extensions.DependencyInjection;
using Stargazer.Abp.Authentication.Domain;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Stargazer.Abp.Authentication.EntityFrameworkCore
{
    [DependsOn(
        typeof(DomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule))]
    public class EntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EntityFrameworkCoreDbContext>(options => {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseNpgsql();
            });
            Configure<AbpClockOptions>(options => { options.Kind = DateTimeKind.Utc; });
        }
    }
}