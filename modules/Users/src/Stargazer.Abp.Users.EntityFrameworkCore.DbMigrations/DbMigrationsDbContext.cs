using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Users.EntityFrameworkCore.DbMigrations
{    
    [ConnectionStringName("Default")]
    public class DbMigrationsDbContext(DbContextOptions<DbMigrationsDbContext> options)
        : AbpDbContext<DbMigrationsDbContext>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Configure();
            base.OnModelCreating(builder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<DateTimeToUtcConverter>();
            base.ConfigureConventions(configurationBuilder);
        }
        public class DateTimeToUtcConverter : ValueConverter<DateTime, DateTime>
        {
            public DateTimeToUtcConverter()
                : base(
                    v => v.Kind == DateTimeKind.Local ? v.ToUniversalTime() : v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            {
            }
        }
    }
}