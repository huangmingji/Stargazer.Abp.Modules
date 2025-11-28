using Stargazer.Abp.Categories.Domain.Shared.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Stargazer.Abp.Categories.Application.Contracts.Permissions
{
    public class CategoriesPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(CategoriesPermissions.GroupName, L("Permission:Categories"));
            
            var categories = moduleGroup.AddPermission(CategoriesPermissions.Categories.Default, L("Permission:Category"));
            categories.AddChild(CategoriesPermissions.Categories.Manage, L("Permission:Manage"));
            categories.AddChild(CategoriesPermissions.Categories.Create, L("Permission:Create"));
            categories.AddChild(CategoriesPermissions.Categories.Update, L("Permission:Update"));
            categories.AddChild(CategoriesPermissions.Categories.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CategoriesResource>(name);
        }
    }
}
