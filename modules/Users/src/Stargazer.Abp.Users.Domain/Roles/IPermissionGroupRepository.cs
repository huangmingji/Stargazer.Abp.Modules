using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Users.Domain.Roles;

public interface IPermissionGroupRepository: IRepository<PermissionGroup, Guid>
{
    
}