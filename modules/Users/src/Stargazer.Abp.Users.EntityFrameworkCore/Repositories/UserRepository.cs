using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Users.Domain.Users;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Users.EntityFrameworkCore.Repositories
{
    public class UserRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider)
        : EfCoreRepository<EntityFrameworkCoreDbContext, UserData, Guid>(dbContextProvider), IUserRepository
    {
        public override async Task<IQueryable<UserData>> WithDetailsAsync()
        {
            var queryable = await GetQueryableAsync();
            return queryable.Include(x => x.UserRoles)
                .ThenInclude(x => x.RoleData)
                .ThenInclude(x => x.Permissions)
                .ThenInclude(x=> x.PermissionData);
        }

        public async Task<List<UserData>> GetListByPageAsync(Expression<Func<UserData, bool>> expression, int pageIndex,
            int pageSize)
        {
            var queryable = await GetQueryableAsync();
            var data = queryable.Where(expression)
                .OrderByDescending(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return data;
        }
    }
}