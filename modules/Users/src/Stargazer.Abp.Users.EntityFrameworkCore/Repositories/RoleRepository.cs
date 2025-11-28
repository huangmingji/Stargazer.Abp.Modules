using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Users.Domain.Roles;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Users.EntityFrameworkCore.Repositories;

public class RoleRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider)
    : EfCoreRepository<EntityFrameworkCoreDbContext, RoleData, Guid>(dbContextProvider), IRoleRepository
{
    public override async Task<IQueryable<RoleData>> WithDetailsAsync()
    {
        var queryable = await GetQueryableAsync();
        return queryable.Include(x => x.Permissions).ThenInclude(x => x.PermissionData);
    }

    public async Task CheckNotNull(string name, Guid? id = null)
    {
        var queryable = await GetQueryableAsync();
        if (queryable.Where(x => x.Name == name).WhereIf(id != null, x => x.Id != id).Any())
        {
            throw new RoleNotNullException(id, name);
        }
    }
}