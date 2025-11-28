using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Users.Domain.Roles
{
    public interface IPermissionRepository: IRepository<PermissionData, Guid>
    {
        
    }
}