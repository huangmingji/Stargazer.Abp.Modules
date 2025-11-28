using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Authentication.Domain.Authentication;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Stargazer.Abp.Authentication.EntityFrameworkCore
{
    public static class EntityFrameworkCoreDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            
            builder.Entity<UserDevice>(b =>
            {
                b.ToTable("UserDevices");
                b.HasKey(o => o.Id);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

            builder.Entity<UserSession>(b =>
            {
                b.ToTable("UserSessions");
                b.HasKey(o => o.Id);
                b.HasOne(o => o.Device)
                    .WithMany()
                    .HasForeignKey(o => o.DeviceId);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });
        }
    }
}