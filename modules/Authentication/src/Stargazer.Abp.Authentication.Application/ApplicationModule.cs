using Microsoft.Extensions.DependencyInjection;
using Stargazer.Abp.Authentication.Application.Contracts;
using Stargazer.Abp.Authentication.Domain;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Authentication.Application
{
    [DependsOn(
        typeof(ApplicationContractsModule),
        typeof(DomainModule),
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
    }
}