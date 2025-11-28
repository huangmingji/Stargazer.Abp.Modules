using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Categories.Domain;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Stargazer.Abp.Categories.EntityFrameworkCore
{
    public static class EntityFrameworkCoreDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<Category>(b =>
            {
                b.ToTable("Categories");
                b.ConfigureByConvention(); 
                b.HasKey(x => x.Id);
            });

        }
    }
}