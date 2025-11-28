using Stargazer.Abp.Authentication.Application.Contracts;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Authentication.HttpApi
{
    [DependsOn(
        typeof(ApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
    public class HttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}