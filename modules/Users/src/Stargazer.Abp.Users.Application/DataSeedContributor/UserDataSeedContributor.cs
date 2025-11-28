using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;
using Stargazer.Abp.Users.Domain.Roles;
using Stargazer.Abp.Users.Domain.Shared.Localization.Resources;
using Stargazer.Abp.Users.Domain.Users;
using Stargazer.Common;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Users.Application.DataSeedContributor;

public class UserDataSeedContributor(
    IPermissionGroupService permissionGroupService,
    ICurrentTenant currentTenant,
    IGuidGenerator guidGenerator,
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    ILogger<UserDataSeedContributor> logger,
    IStringLocalizer<UserResource> localizer)
    : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        using (currentTenant.Change(context?.TenantId))
        {
            await InitPermission();
            await InitRole();
            await InitUser();
        }
    }

    private PermissionGroupDto? userGroup;
    private async Task InitPermission()
    {
        var groups = await permissionGroupService.GetListAsync();
        userGroup = groups.FirstOrDefault(x => x.Permission == UsersPermissions.GroupName);
        if (userGroup == null)
        {
            userGroup = await permissionGroupService.CreateAsync(new CreateOrUpdatePermissionGroupDto()
            {
                Name = localizer["permission_users"],
                Permission = UsersPermissions.GroupName
            });
        }
        {
            var data = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Users.Default);
            if (data == null)
            {
                data = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = userGroup.Id,
                    Name = localizer["permission_user"].ToString(),
                    Permission = UsersPermissions.Users.Default
                });
                userGroup.Permissions.Add(data);
            }
            Thread.Sleep(100);
            var userManage = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Users.Manage);
            if (userManage == null)
            {
                userManage = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_user_manage"].ToString(),
                    Permission = UsersPermissions.Users.Manage
                });
                userGroup.Permissions.Add(userManage);
            }
            Thread.Sleep(100);

            var createUser = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Users.Create);
            if (createUser == null)
            {
                createUser = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_user_create"].ToString(),
                    Permission = UsersPermissions.Users.Create
                });
                userGroup.Permissions.Add(createUser);
            }
            Thread.Sleep(100);

            var updateUser = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Users.Update);
            if (updateUser == null)
            {
                updateUser = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_user_update"].ToString(),
                    Permission = UsersPermissions.Users.Update
                });
                userGroup.Permissions.Add(updateUser);
            }
            Thread.Sleep(100);

            var deleteUser = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Users.Delete);
            if (deleteUser == null)
            {
                deleteUser = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_user_delete"].ToString(),
                    Permission = UsersPermissions.Users.Delete
                });
                userGroup.Permissions.Add(deleteUser);
            }
            Thread.Sleep(100);
        }

        {
            var data = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Roles.Default);
            if (data == null)
            {
                data = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = userGroup.Id,
                    Name = localizer["permission_role"].ToString(),
                    Permission = UsersPermissions.Roles.Default
                });
                userGroup.Permissions.Add(data);
            }
            Thread.Sleep(100);
            
            var roleManage = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Roles.Manage);
            if (roleManage == null)
            {
                roleManage = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_role_manage"].ToString(),
                    Permission = UsersPermissions.Roles.Manage
                });
                userGroup.Permissions.Add(roleManage);
            }
            Thread.Sleep(100);

            var createRole = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Roles.Create);
            if (createRole == null)
            {
                createRole = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_role_create"].ToString(),
                    Permission = UsersPermissions.Roles.Create
                });
                userGroup.Permissions.Add(createRole);
            }
            Thread.Sleep(100);

            var updateRole = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Roles.Update);
            if (updateRole == null)
            {
                updateRole = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_role_update"].ToString(),
                    Permission = UsersPermissions.Roles.Update
                });
                userGroup.Permissions.Add(updateRole);
            }
            Thread.Sleep(100);

            var deleteRole = userGroup.Permissions.FirstOrDefault(x => x.Permission == UsersPermissions.Roles.Delete);
            if (deleteRole == null)
            {
                deleteRole = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    ParentId = data.Id,
                    GroupId = userGroup.Id,
                    Name = localizer["permission_role_delete"].ToString(),
                    Permission = UsersPermissions.Roles.Delete
                });
                userGroup.Permissions.Add(deleteRole);
            }
            Thread.Sleep(100);
        }
    }

    private async Task InitRole()
    {
        string roleName = localizer["user_center_manager"].ToString();
        var role = await roleRepository.FindAsync(x => x.Name == roleName);
        if (role == null)
        {
            role = new RoleData(guidGenerator.Create(), roleName, false, true, true);
            role = await roleRepository.InsertAsync(role, true);
        }
        foreach (var item in userGroup.Permissions)
        {
            if (role.Permissions.All(x => x.PermissionId != item.Id))
            {
                role.Permissions.Add(new RolePermissionData(guidGenerator.Create(), role.Id, item.Id));
            }
        }
        _ = await roleRepository.UpdateAsync(role);
    }

    private async Task InitUser()
    {
        var user = await userRepository.FindAsync(x => x.Account == "admin");
        if(user == null)
        {
            user = new UserData(guidGenerator.Create(), account:"admin", nickName:"admin");
            user.SetPassword("Admin12345678");
            string roleName = localizer["user_center_manager"];
            var role = await roleRepository.GetAsync(x => x.Name == roleName);
            user.AddRole(guidGenerator.Create(), role.Id);
            user = await userRepository.InsertAsync(user);
            logger.LogInformation($"account--{user.Account}");
            logger.LogInformation($"password--{user.Password}");
            logger.LogInformation($"secretKey--{user.SecretKey}");
            if (Cryptography.PasswordStorage.VerifyPassword("Admin12345678", user.Password, user.SecretKey))
            {
                logger.LogInformation($"password--{user.Password}");
            }
        }
    }
}