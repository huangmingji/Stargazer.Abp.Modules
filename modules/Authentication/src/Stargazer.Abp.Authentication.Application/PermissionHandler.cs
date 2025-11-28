using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Stargazer.Common.Extend;

namespace Stargazer.Abp.Authentication.Application;

public class PermissionHandler(ILogger<PermissionHandler> logger) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context.User.FindFirst(x => x.Type == "permissions");
        logger.LogInformation("PermissionHandler-HandleRequirementAsync: {0}", permissions);
        if (permissions != null && !permissions.Value.DeserializeObject<List<string>>().Contains(requirement.Permission))
        {
            context.Fail();
            return;
        }
            
        context.Succeed(requirement);
        await Task.CompletedTask;
    }

    public override async Task HandleAsync(AuthorizationHandlerContext context)
    {
        logger.LogInformation("PermissionHandler-HandleAsync: {0}", context.User.FindFirst(x => x.Type == "permissions"));
        await base.HandleAsync(context);
    }
}