using Stargazer.Abp.Users.Application.Contracts;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Users.HttpApi
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