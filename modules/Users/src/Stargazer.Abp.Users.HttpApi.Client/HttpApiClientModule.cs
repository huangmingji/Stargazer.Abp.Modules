using Microsoft.Extensions.DependencyInjection;
using Stargazer.Abp.Users.Application.Contracts;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Stargazer.Abp.Users.HttpApi.Client
{
    [DependsOn(
        typeof(AbpVirtualFileSystemModule),
        typeof(AbpHttpClientModule),
        typeof(ApplicationContractsModule))
    ]
    public class HttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            
            context.Services.AddHttpClientProxies(
                typeof(ApplicationContractsModule).Assembly,
                remoteServiceConfigurationName: "Users");
        }
    }
}
