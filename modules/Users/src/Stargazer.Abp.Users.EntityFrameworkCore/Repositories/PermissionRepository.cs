using Stargazer.Abp.Users.Domain.Roles;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Stargazer.Abp.Users.EntityFrameworkCore.Repositories;

public class PermissionRepository(IDbContextProvider<EntityFrameworkCoreDbContext> dbContextProvider)
    : EfCoreRepository<EntityFrameworkCoreDbContext, PermissionData, Guid>(dbContextProvider), IPermissionRepository;