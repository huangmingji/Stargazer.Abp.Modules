using Stargazer.Abp.Authentication.Domain.Shared;
using Stargazer.Abp.Authentication.Domain.Shared.MultiTenancy;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Authentication.Domain
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