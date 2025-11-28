using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Users.Domain.Roles;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Users.EntityFrameworkCore.Repositories;

public class PermissionGroupRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider)
    : EfCoreRepository<EntityFrameworkCoreDbContext, PermissionGroup, Guid>(dbContextProvider),
        IPermissionGroupRepository
{
    public override async Task<IQueryable<PermissionGroup>> WithDetailsAsync()
    {
        var queryable = await GetQueryableAsync();
        return queryable.Include(x => x.Permissions);
    }
}