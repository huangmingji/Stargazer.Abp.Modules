using Volo.Abp.Reflection;

namespace Stargazer.Abp.Users.Application.Contracts
{
    public class UsersPermissions
    {
        public const string GroupName = "users";

        public static class Users
        {
            public const string Default = GroupName + "_user";
            public const string Manage = Default + "_manage";
            public const string Delete = Default + "_delete";
            public const string Update = Default + "_update";
            public const string Create = Default + "_create";
        }

        public static class Roles
        {
            public const string Default = GroupName + "_role";
            public const string Manage = Default + "_manage";
            public const string Delete = Default + "_delete";
            public const string Update = Default + "_update";
            public const string Create = Default + "_create";
        }

        public static class Permission
        {
            public const string Default = GroupName + "_permission";
            public const string Manage = Default + "_manage";
            public const string Delete = Default + "_delete";
            public const string Update = Default + "_update";
            public const string Create = Default + "_create";
        }
        
        
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(UsersPermissions));
        }
    }
}
