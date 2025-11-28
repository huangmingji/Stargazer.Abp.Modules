using Stargazer.Abp.Authentication.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.FluentValidation;
using Volo.Abp.Modularity;

namespace Stargazer.Abp.Authentication.Application.Contracts
{
    [DependsOn(
        typeof(Users.Application.Contracts.ApplicationContractsModule),
        typeof(DomainSharedModule),
        typeof(AbpFluentValidationModule),
        typeof(AbpDddApplicationContractsModule)
    )]
    public class ApplicationContractsModule : AbpModule
    {
        
    }
}