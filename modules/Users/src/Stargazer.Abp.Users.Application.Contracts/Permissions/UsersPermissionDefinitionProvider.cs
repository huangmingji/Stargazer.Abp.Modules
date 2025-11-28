using Stargazer.Abp.Users.Domain.Shared.Localization.Resources;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Stargazer.Abp.Users.Application.Contracts.Permissions
{
    public class UsersPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(UsersPermissions.GroupName, L("permission_users"));
            
            var users = moduleGroup.AddPermission(UsersPermissions.Users.Default, L("permission_user"));
            users.AddChild(UsersPermissions.Users.Manage, L("permission_manage"));
            users.AddChild(UsersPermissions.Users.Create, L("permission_create"));
            users.AddChild(UsersPermissions.Users.Update, L("permission_update"));
            users.AddChild(UsersPermissions.Users.Delete, L("permission_delete"));

            var product = moduleGroup.AddPermission(UsersPermissions.Roles.Default, L("permission_role"));
            product.AddChild(UsersPermissions.Roles.Manage, L("permission_manage"));
            product.AddChild(UsersPermissions.Roles.Create, L("permission_create"));
            product.AddChild(UsersPermissions.Roles.Update, L("permission_update"));
            product.AddChild(UsersPermissions.Roles.Delete, L("permission_delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<UserResource>(name);
        }
    }
}
