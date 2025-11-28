using Stargazer.Abp.Categories.Domain.Shared;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Categories.Domain
{
    [DependsOn(
        typeof(DomainSharedModule),
        typeof(AbpDddDomainModule))]
    public class DomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}