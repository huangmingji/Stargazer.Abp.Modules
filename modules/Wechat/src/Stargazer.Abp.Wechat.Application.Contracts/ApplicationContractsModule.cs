using Stargazer.Abp.ObjectStorage.Application.Contracts;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Wechat.Application
{
    [DependsOn(
        typeof(StargazerAbpObjectStorageApplicationContractsModule),
        typeof(AbpFluentValidationModule),
        typeof(AbpDddApplicationContractsModule)
    )]
    public class ApplicationContractsModule : AbpModule
    {
        
    }
}