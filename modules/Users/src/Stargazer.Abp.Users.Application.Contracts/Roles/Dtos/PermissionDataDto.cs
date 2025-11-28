namespace Stargazer.Abp.Users.Application.Contracts.Roles.Dtos
{
    public class PermissionDataDto
    {
        public Guid Id { get; set; }
        
        /// <summary>
        /// 上级主键
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; } = "";
        
        /// <summary>
        /// 权限
        /// </summary>
        public string Permission { get; set; } = "";

    }
}