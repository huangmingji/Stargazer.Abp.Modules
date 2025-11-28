using Microsoft.AspNetCore.Authorization;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;
using Stargazer.Abp.Users.Domain.Roles;
using Volo.Abp.Application.Services;

namespace Stargazer.Abp.Users.Application.Roles
{
    [Authorize]
    public class RoleService : ApplicationService, IRoleService
    {
        private IRoleRepository RoleRepository => this.LazyServiceProvider.LazyGetRequiredService<IRoleRepository>();
        private IPermissionRepository PermissionRepository => this.LazyServiceProvider.LazyGetRequiredService<IPermissionRepository>();

        [Authorize(UsersPermissions.Roles.Create)]
        public async Task<RoleDto> CreatePrivateAsync(UpdateRoleDto input)
        {
            await RoleRepository.CheckNotNull(input.Name);
            var roleData = new RoleData(
                GuidGenerator.Create(), input.Name, input.IsDefault, false, false);
            input.PermissionIds?.ForEach(item =>
            {
                roleData.Permissions.Add(new RolePermissionData(GuidGenerator.Create(), roleData.Id, item));
            });
            
            if (input.IsDefault)
            {
                var defaultRole = await RoleRepository.FindAsync(x => x.IsDefault);
                if (defaultRole != null)
                {
                    defaultRole.IsDefault = false;
                    await RoleRepository.UpdateAsync(defaultRole);
                }
            }

            var result = await RoleRepository.InsertAsync(roleData);
            return ObjectMapper.Map<RoleData, RoleDto>(result);
        }

        [Authorize(UsersPermissions.Roles.Create)]
        public async Task<RoleDto> CreatePublicAsync(UpdateRoleDto input)
        {
            await RoleRepository.CheckNotNull(input.Name);
            var roleData = new RoleData(
                GuidGenerator.Create(), input.Name, input.IsDefault, false, true);
            input.PermissionIds?.ForEach(item =>
            {
                roleData.Permissions.Add(new RolePermissionData(GuidGenerator.Create(), roleData.Id, item));
            });
            
            if (input.IsDefault)
            {
                var defaultRole = await RoleRepository.FindAsync(x => x.IsDefault);
                if (defaultRole != null)
                {
                    defaultRole.IsDefault = false;
                    await RoleRepository.UpdateAsync(defaultRole);
                }
            }

            var result = await RoleRepository.InsertAsync(roleData);
            return ObjectMapper.Map<RoleData, RoleDto>(result);
        }

        [Authorize(UsersPermissions.Roles.Manage)]
        public async Task<RoleDto> FindAsync(string name)
        {
            var data = await RoleRepository.FindAsync(x => x.Name == name);
            return ObjectMapper.Map<RoleData, RoleDto>(data);   
        }

        [Authorize(UsersPermissions.Roles.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await RoleRepository.DeleteAsync(x=> x.Id == id);
        }

        [Authorize(UsersPermissions.Roles.Manage)]
        public async Task<List<PermissionDataDto>> GetPermissionsAsync()
        {
            var data = await PermissionRepository.GetListAsync();
            return ObjectMapper.Map<List<PermissionData>, List<PermissionDataDto>>(data);
        }

        [Authorize(UsersPermissions.Roles.Manage)]
        public async Task<List<RoleDto>> GetListAsync()
        {
            var data = await RoleRepository.GetListAsync(true);
            return ObjectMapper.Map<List<RoleData>, List<RoleDto>>(data);
        }

        [Authorize(UsersPermissions.Roles.Manage)]
        public async Task<RoleDto> GetAsync(Guid id)
        {
            await RoleRepository.GetAsync(id);
            var data = await RoleRepository.GetAsync(x => x.Id == id);
            return ObjectMapper.Map<RoleData, RoleDto>(data);       
        }

        [Authorize(UsersPermissions.Roles.Update)]
        public async Task<RoleDto> UpdateAsync(Guid id ,UpdateRoleDto input)
        {
            await RoleRepository.CheckNotNull(input.Name, id);
            var roleData = await RoleRepository.GetAsync(x => x.Id == id);
            roleData.Name = input.Name;
            roleData.Permissions.RemoveAll(x => !input.PermissionIds.Contains(x.PermissionId));
            input.PermissionIds?.ForEach(item => {
                if (!roleData.Permissions.Exists(x => x.PermissionId == item))
                {
                    roleData.Permissions.Add(new RolePermissionData(GuidGenerator.Create(), roleData.Id, item));
                }
            });
            
            if (input.IsDefault)
            {
                var defaultRole = await RoleRepository.FindAsync(x => x.IsDefault);
                if (defaultRole != null)
                {
                    defaultRole.IsDefault = false;
                    await RoleRepository.UpdateAsync(defaultRole);
                }
            }
            
            var result = await RoleRepository.UpdateAsync(roleData);
            return ObjectMapper.Map<RoleData, RoleDto>(roleData);
        }
    }
}
