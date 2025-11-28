using Microsoft.AspNetCore.Authorization;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;
using Stargazer.Abp.Users.Domain.Roles;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Stargazer.Abp.Users.Application.Roles;

[Authorize]
public class PermissionGroupService : ApplicationService, IPermissionGroupService
{
    private IPermissionGroupRepository PermissionGroupRepository =>
        this.LazyServiceProvider.LazyGetRequiredService<IPermissionGroupRepository>();
    private IPermissionRepository PermissionRepository =>
        this.LazyServiceProvider.LazyGetRequiredService<IPermissionRepository>();
    
    public async Task<List<PermissionGroupDto>> GetListAsync(CancellationToken cancellationToken = default)
    {
        var data = await PermissionGroupRepository.GetListAsync(true, cancellationToken: cancellationToken);
        return ObjectMapper.Map<List<PermissionGroup>, List<PermissionGroupDto>>(data);
    }

    public async Task<PermissionGroupDto> CreateAsync(CreateOrUpdatePermissionGroupDto input, CancellationToken cancellationToken = default)
    {
        if (await PermissionGroupRepository.AnyAsync(x => x.Permission == input.Permission, cancellationToken: cancellationToken))
        {
            throw new UserFriendlyException($"permission group {input.Permission} already exist.");
        }

        var data = new PermissionGroup(GuidGenerator.Create(), input.Name, input.Permission);
        return ObjectMapper.Map<PermissionGroup, PermissionGroupDto>(
            await PermissionGroupRepository.InsertAsync(data, cancellationToken: cancellationToken));
    }

    public async Task<PermissionDataDto> CreatePermissionAsync(CreateOrUpdatePermissionDto input, CancellationToken cancellationToken = default)
    {
        if (await PermissionRepository.AnyAsync(x => x.Permission == input.Permission, cancellationToken))
        {
            throw new UserFriendlyException($"permission {input.Permission} already exist.");
        }
        var data = new PermissionData(GuidGenerator.Create(), input.GroupId, input.Name, input.Permission, input.ParentId);
        return ObjectMapper.Map<PermissionData, PermissionDataDto>(
            await PermissionRepository.InsertAsync(data, cancellationToken: cancellationToken));
    }
}