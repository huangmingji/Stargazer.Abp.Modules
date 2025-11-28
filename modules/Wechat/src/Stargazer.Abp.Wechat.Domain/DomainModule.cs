using Stargazer.Abp.Wechat.Domain.Shared;
using Stargazer.Abp.Wechat.Domain.Shared.MultiTenancy;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Wechat.Domain
{
    [DependsOn(
        typeof(DomainSharedModule),
        typeof(AbpDddDomainModule))]
    public class DomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });
        }
    }
}