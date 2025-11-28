using Stargazer.Abp.Users.Application;
using Stargazer.Abp.Users.EntityFrameworkCore.DbMigrations;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Users.DbMigrations;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EntityFrameworkCoreDbMigrationsModule),
    typeof(ApplicationModule)
)]
public class DbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        
    }
}