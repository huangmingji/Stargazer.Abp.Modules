using Stargazer.Abp.Authentication.Domain.Authentication;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Authentication.EntityFrameworkCore.Repositories;

public class UserDeviceRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider)
    : EfCoreRepository<EntityFrameworkCoreDbContext, UserDevice, Guid>(dbContextProvider), IUserDeviceRepository
{
    
}