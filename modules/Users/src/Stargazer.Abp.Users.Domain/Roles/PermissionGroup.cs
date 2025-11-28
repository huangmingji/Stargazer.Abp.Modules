using MimeKit;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Stargazer.Abp.Users.Domain.Roles;

public class PermissionGroup: AuditedAggregateRoot<Guid>
{
    public PermissionGroup(Guid id, string name, string permission): base(id)
    {
        Name = name;
        Permission = permission;
    }
    
    public string Name { get; protected set; }
    
    public string Permission { get; protected set; }
    
    public List<PermissionData> Permissions { get; set; } = new List<PermissionData>();
}