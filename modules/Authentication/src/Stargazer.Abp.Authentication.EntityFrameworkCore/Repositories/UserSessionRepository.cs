using Microsoft.EntityFrameworkCore;
using Stargazer.Abp.Authentication.Domain.Authentication;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Authentication.EntityFrameworkCore.Repositories;

public class UserSessionRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider)
    : EfCoreRepository<EntityFrameworkCoreDbContext, UserSession, Guid>(dbContextProvider), IUserSessionRepository
{
    
     public override async Task<IQueryable<UserSession>> WithDetailsAsync()
    {
        return (await GetQueryableAsync())
            .Include(x => x.Device);
    }
    
}