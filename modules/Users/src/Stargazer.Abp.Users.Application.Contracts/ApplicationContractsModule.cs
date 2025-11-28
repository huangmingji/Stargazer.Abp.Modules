using Stargazer.Abp.Users.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Users.Application.Contracts
{
    [DependsOn(
        typeof(DomainSharedModule),
        typeof(AbpFluentValidationModule),
        typeof(AbpDddApplicationContractsModule)
    )]
    public class ApplicationContractsModule : AbpModule
    {
        
    }
}