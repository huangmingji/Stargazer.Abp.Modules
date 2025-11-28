using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stargazer.Abp.Categories.Domain;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Categories.EntityFrameworkCore
{    
    [ConnectionStringName("Default")]
    public class EntityFrameworkCoreDbContext(DbContextOptions<EntityFrameworkCoreDbContext> options)
        : AbpDbContext<EntityFrameworkCoreDbContext>(options)
    {
        public DbSet<Category> Categories { get; set; }
        
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