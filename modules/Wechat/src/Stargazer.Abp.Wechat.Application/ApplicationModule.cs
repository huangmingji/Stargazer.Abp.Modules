using Microsoft.Extensions.DependencyInjection;
using Stargazer.Abp.ObjectStorage.Application;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Wechat.Application
{
    [DependsOn(
        typeof(StargazerAbpObjectStorageApplicationModule),
        typeof(ApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
    )]
    public class ApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddAutoMapperObjectMapper<ApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<ApplicationAutoMapperProfile>(validate: true);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }
    }
}