using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Users.Domain.Roles;
using Stargazer.Abp.Users.Domain.Users;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Stargazer.Abp.Users.EntityFrameworkCore
{
    public static class EntityFrameworkCoreDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            builder.ConfigureAccount();
        }
        private static void ConfigureAccount(this ModelBuilder builder)
        {
            builder.Entity<PermissionGroup>(b =>
            {
                b.ToTable("PermissionGroup");
                b.HasKey(o => o.Id);
                b.HasMany(x => x.Permissions)
                    .WithOne()
                    .HasForeignKey(x => x.GroupId);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });
            builder.Entity<RoleData>(b =>
            {
                b.ToTable("RoleData");
                b.HasKey(o => o.Id);
                b.HasMany(x => x.Permissions)
                    .WithOne()
                    .HasForeignKey(x => x.RoleId);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

            builder.Entity<RolePermissionData>(b =>
            {
                b.ToTable("RolePermissionData");
                b.HasKey(x => x.Id);
                b.HasOne(x => x.PermissionData)
                    .WithMany()
                    .HasForeignKey(x => x.PermissionId);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

            builder.Entity<PermissionData>(b =>
            {
                b.ToTable("PermissionData");
                b.HasKey(o => o.Id);
                b.ConfigureByConvention();
            });

            builder.Entity<UserData>(b =>
            {
                b.ToTable("UserData");
                b.HasKey(o => o.Id);
                b.HasMany(x => x.UserRoles)
                    .WithOne()
                    .HasForeignKey(x => x.UserId);
                b.HasIndex(o => o.Account).IsUnique();
                b.HasIndex(x => x.Email).IsUnique();
                b.HasIndex(x => x.PhoneNumber).IsUnique();
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

            builder.Entity<UserRole>(b =>
            {
                b.ToTable("UserRole");
                b.HasKey(x => x.Id);
                b.HasOne(x => x.RoleData)
                    .WithMany()
                    .HasForeignKey(x => x.RoleId);
                b.HasIndex(o => o.UserId);
                b.HasIndex(o => new { o.RoleId, o.UserId }).IsUnique();
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });
        }
    }
}