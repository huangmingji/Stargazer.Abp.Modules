using Microsoft.Extensions.DependencyInjection;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Application.Users;
using Stargazer.Abp.Users.Domain;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.MailKit;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Users.Application
{
    [DependsOn(
        typeof(ApplicationContractsModule),
        typeof(DomainModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpMailKitModule),
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
            context.Services.AddTransient<EmailService>();
        }
    }
}