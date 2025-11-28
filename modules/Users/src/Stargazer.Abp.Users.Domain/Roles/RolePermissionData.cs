using Volo.Abp.Domain.Entities.Auditing;

namespace Stargazer.Abp.Users.Domain.Roles
{
    public class RolePermissionData(Guid id, Guid roleId, Guid permissionId)
        : AuditedAggregateRoot<Guid>(id)
    {
        public Guid RoleId { get; protected set; } = roleId;

        public Guid PermissionId { get; protected set; } = permissionId;

        public PermissionData PermissionData { get; protected set; }
    }
}