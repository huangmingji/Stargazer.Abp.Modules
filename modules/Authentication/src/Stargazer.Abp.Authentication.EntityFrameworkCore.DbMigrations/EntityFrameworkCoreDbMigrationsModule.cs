using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Authentication.EntityFrameworkCore.DbMigrations
{
    [DependsOn(
        typeof(EntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule))]
    public class EntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<DbMigrationsDbContext>(options => {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseNpgsql();
            });

            #region 自动迁移数据库

            var  dbMigrationsDbContext =  context.Services.BuildServiceProvider().GetService<DbMigrationsDbContext>();
            if (dbMigrationsDbContext != null)
            {
                dbMigrationsDbContext.Database.Migrate();
            }
            
            #endregion 自动迁移数据库
        }
    }
}