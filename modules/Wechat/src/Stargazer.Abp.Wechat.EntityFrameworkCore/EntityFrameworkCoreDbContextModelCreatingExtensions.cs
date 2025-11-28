using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Wechat.Domain.Wechat;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Stargazer.Abp.Wechat.EntityFrameworkCore
{
    public static class EntityFrameworkCoreDbContextModelCreatingExtensions
    {
        public static void Configure(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<WechatData>(b =>
            {
                b.ToTable(nameof(WechatData));
                b.HasKey(o => o.Id);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

            builder.Entity<WechatAccessTokenData>(b =>
            {
                b.ToTable(nameof(WechatAccessTokenData));
                b.HasKey(o => o.Id);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

            builder.Entity<WechatJsapiTicketData>(b =>
            {
                b.ToTable(nameof(WechatJsapiTicketData));
                b.HasKey(o => o.Id);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

            builder.Entity<WechatUserData>(b => {
                b.ToTable(nameof(WechatUserData));
                b.HasKey(o => o.Id);
                b.ConfigureAuditedAggregateRoot();
                b.ConfigureByConvention();
            });

        }
    }
}