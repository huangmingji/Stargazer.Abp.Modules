using Microsoft.Extensions.Localization;
using Stargazer.Abp.Categories.Application.Contracts.Permissions;
using Stargazer.Abp.Categories.Domain.Shared.Localization;
using Stargazer.Abp.Users.Application.Contracts;
using Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Categories.Application.DataSeedContributor;

public class CategoryDataSeedContributor(
    IPermissionGroupService permissionGroupService,
    ICurrentTenant currentTenant,
    IStringLocalizer<CategoriesResource> localizer)
    : IDataSeedContributor, ITransientDependency
{

    public async Task SeedAsync(DataSeedContext context)
    {
        using (currentTenant.Change(context?.TenantId))
        {
            await InitPermission();
        }
    }
    private PermissionGroupDto? group;

    private async Task InitPermission()
    {
        var groups = await permissionGroupService.GetListAsync();
        group = groups.FirstOrDefault(x => x.Permission == CategoriesPermissions.GroupName);
        if (group == null)
        {
            group = await permissionGroupService.CreateAsync(new CreateOrUpdatePermissionGroupDto()
            {
                Name = localizer["Permission:Products"],
                Permission = CategoriesPermissions.GroupName
            });
        }

        {
            var data = group.Permissions.FirstOrDefault(x => x.Permission == CategoriesPermissions.Categories.Default);
            if (data == null)
            {
                data = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = group.Id,
                    Name = localizer["Permission:Category"].ToString(),
                    Permission = CategoriesPermissions.Categories.Default
                });
                group.Permissions.Add(data);
            }
            var manage = group.Permissions.FirstOrDefault(x => x.Permission == CategoriesPermissions.Categories.Manage);
            if (manage == null)
            {
                manage  = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = group.Id,
                    ParentId = data.Id,
                    Name = localizer["Permission:Manage"].ToString(),
                    Permission = CategoriesPermissions.Categories.Manage
                });
                group.Permissions.Add(manage);
            }
            
            var create = group.Permissions.FirstOrDefault(x => x.Permission == CategoriesPermissions.Categories.Create);
            if (create == null)
            {
                create = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = group.Id,
                    ParentId = data.Id,
                    Name = localizer["Permission:Create"].ToString(),
                    Permission = CategoriesPermissions.Categories.Create
                });
                group.Permissions.Add(create);
            }
            var update = group.Permissions.FirstOrDefault(x => x.Permission == CategoriesPermissions.Categories.Update);
            if (update == null)
            {
                update  = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = group.Id,
                    ParentId = data.Id,
                    Name = localizer["Permission:Update"].ToString(),
                    Permission = CategoriesPermissions.Categories.Update
                });
                group.Permissions.Add(update);
            }
            var delete = group.Permissions.FirstOrDefault(x => x.Permission == CategoriesPermissions.Categories.Delete);
            if (delete == null)
            {
                delete = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = group.Id,
                    ParentId = data.Id,
                    Name = localizer["Permission:Delete"].ToString(),
                    Permission = CategoriesPermissions.Categories.Delete
                });
                group.Permissions.Add(delete);
            }

            var showhide =
                group.Permissions.FirstOrDefault(x => x.Permission == CategoriesPermissions.Categories.ShowHidden);
            if (showhide == null)
            {
                showhide  = await permissionGroupService.CreatePermissionAsync(new CreateOrUpdatePermissionDto()
                {
                    GroupId = group.Id,
                    ParentId = data.Id,
                    Name = localizer["Permission:ShowHidden"].ToString(),
                    Permission = CategoriesPermissions.Categories.ShowHidden
                });
                group.Permissions.Add(showhide);
            }
        }
    }
}