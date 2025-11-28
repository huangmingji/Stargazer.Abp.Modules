using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Stargazer.Abp.Users.Domain.Roles
{
    /// <summary>
    /// 权限
    /// </summary>
    public sealed class PermissionData : AuditedEntity<Guid>
    {
        public PermissionData(Guid id, Guid groupId, string name, string permission, Guid? parentId = null): base(id)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(permission, nameof(permission));
            ParentId = parentId;
            GroupId = groupId;
            Name = name;
            Permission = permission;
        }

        public void Set(string name, string permission, Guid? parentId = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(permission, nameof(permission));
            ParentId = parentId;
            Name = name;
            Permission = permission;
        }

        /// <summary>
        /// 上级主键
        /// </summary>
        public Guid? ParentId { get; set; }

        public Guid GroupId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 权限
        /// </summary>
        public string Permission { get; set; }
    }
}