namespace Stargazer.Abp.Users.Application.Contracts.Roles.Dtos
{
    public class RolePermissionDto 
    {

        public Guid Id { get; set; }
        
        public Guid RoleId { get; set; }

        public Guid PermissionId { get; set; }

        public PermissionDataDto PermissionData { get; set; }
    }
}