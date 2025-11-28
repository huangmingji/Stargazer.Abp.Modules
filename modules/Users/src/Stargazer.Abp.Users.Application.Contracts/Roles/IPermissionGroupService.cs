using Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Users.Application.Contracts;

public interface IPermissionGroupService: IApplicationService
{
    Task<List<PermissionGroupDto>> GetListAsync(CancellationToken cancellationToken = default);
    
    Task<PermissionGroupDto> CreateAsync(CreateOrUpdatePermissionGroupDto input, CancellationToken cancellationToken = default);

    Task<PermissionDataDto> CreatePermissionAsync(CreateOrUpdatePermissionDto input, CancellationToken cancellationToken = default);
}