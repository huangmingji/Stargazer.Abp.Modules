using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stargazer.Abp.Authentication.Domain.Authentication;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Authentication.EntityFrameworkCore
{    
    [ConnectionStringName("Default")]
    public class EntityFrameworkCoreDbContext: AbpDbContext<EntityFrameworkCoreDbContext>
    {
        
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }

        public EntityFrameworkCoreDbContext(DbContextOptions<EntityFrameworkCoreDbContext> options)
            : base(options)
        {

        }

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