using Volo.Abp;

namespace Stargazer.Abp.Users.Domain.Roles
{
    public class RoleNotNullException(Guid? roleId, string roleName) : UserFriendlyException(message: "该角色已存在！",
        details: $"The role ({roleId}) name ({roleName}) already exists)");
}