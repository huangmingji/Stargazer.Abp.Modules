using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace Stargazer.Abp.Wechat.Domain.Shared
{
    [DependsOn(typeof(AbpValidationModule))]
    public class DomainSharedModule : AbpModule
    {
        public DomainSharedModule()
        {
        }
    }
}
