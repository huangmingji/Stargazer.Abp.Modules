using Microsoft.AspNetCore.Authorization;

namespace Stargazer.Abp.Authentication.Application;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}