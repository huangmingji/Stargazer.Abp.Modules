namespace Stargazer.Abp.Users.Application.Contracts.Roles.Dtos;

public class CreateOrUpdatePermissionGroupDto
{
    public string Name { get; set; } = "";
    
    public string Permission { get; set; } = "";
}