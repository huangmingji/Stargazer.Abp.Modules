using Stargazer.Abp.Users.Domain.Shared.Localization.Resources;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace Stargazer.Abp.Users.Domain.Shared
{
    [DependsOn(
        typeof(AbpLocalizationModule),
        typeof(AbpValidationModule))]
    public class DomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DomainSharedModule>();
            });
            
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<UserResource>("zh-Hans")
                    .AddVirtualJson("/Localization/Resources/User");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Stargazer.Abp.Users", typeof(UserResource));
            });
        }
    }
}
