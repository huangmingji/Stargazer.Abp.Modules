using Volo.Abp.Reflection;

namespace Stargazer.Abp.Categories.Application.Contracts.Permissions
{
    public class CategoriesPermissions
    {
        public const string GroupName = "Stargazer.Categories";

        public class Categories
        {
            public const string Default = GroupName + ".Category";
            public const string Manage = Default + ".Manage";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string ShowHidden = Default + ".ShowHidden";
        }
        
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CategoriesPermissions));
        }
    }
}
