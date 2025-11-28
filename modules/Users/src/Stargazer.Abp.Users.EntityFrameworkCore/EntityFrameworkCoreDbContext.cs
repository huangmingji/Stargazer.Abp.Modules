using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stargazer.Abp.Users.Domain.Roles;
using Stargazer.Abp.Users.Domain.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Users.EntityFrameworkCore
{    
    [ConnectionStringName("Default")]
    public class EntityFrameworkCoreDbContext(DbContextOptions<EntityFrameworkCoreDbContext> options)
        : AbpDbContext<EntityFrameworkCoreDbContext>(options)
    {
        public DbSet<UserData> UserData { get; set; }
        public DbSet<RoleData> RoleData { get; set; }
        public DbSet<PermissionData> PermissionData { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<PermissionGroup> PermissionGroup { get; set; }

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