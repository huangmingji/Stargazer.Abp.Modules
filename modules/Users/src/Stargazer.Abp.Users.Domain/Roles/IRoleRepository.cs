using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Users.Domain.Roles
{
    public interface IRoleRepository: IRepository<RoleData, Guid>
    {
        Task CheckNotNull(string name, Guid? id = null);
    }
}