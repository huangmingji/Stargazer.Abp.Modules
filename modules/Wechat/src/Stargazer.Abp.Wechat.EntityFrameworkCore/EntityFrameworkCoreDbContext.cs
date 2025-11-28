using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Wechat.Domain.Wechat;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Wechat.EntityFrameworkCore
{    
    [ConnectionStringName("Default")]
    public class EntityFrameworkCoreDbContext: AbpDbContext<EntityFrameworkCoreDbContext>
    {
        public DbSet<WechatData> WechatDatas { get; set; }
        public DbSet<WechatAccessTokenData> WechatAccessTokenDatas { get; set; }
        public DbSet<WechatJsapiTicketData> WechatJsapiTicketDatas { get; set; }
        public DbSet<WechatUserData> WechatUserDatas { get; set; }

        public EntityFrameworkCoreDbContext(DbContextOptions<EntityFrameworkCoreDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Configure();
            base.OnModelCreating(builder);
        }

    }
}