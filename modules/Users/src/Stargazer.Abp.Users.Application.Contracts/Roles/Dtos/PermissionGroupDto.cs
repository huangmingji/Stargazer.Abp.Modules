namespace Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;

public class PermissionGroupDto
{
    public Guid Id { get; set; }
    
    public string Name { get; protected set; }
    
    public string Permission { get; protected set; }
    
    public List<PermissionDataDto> Permissions { get; set; } = new List<PermissionDataDto>();
}