using Stargazer.Abp.ObjectStorage.HttpApi;
using Stargazer.Abp.Wechat.Application;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Wechat.HttpApi
{
    [DependsOn(
        typeof(StargazerAbpObjectStorageHttpApiModule),
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