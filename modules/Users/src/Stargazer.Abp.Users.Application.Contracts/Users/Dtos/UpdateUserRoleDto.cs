namespace Stargazer.Abp.Users.Application.Contracts
{
    public class UpdateUserRoleDto
    {
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}