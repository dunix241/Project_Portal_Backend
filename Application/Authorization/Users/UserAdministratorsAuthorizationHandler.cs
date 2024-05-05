using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Authorization.Users;

public class UserAdministratorsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, User>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, User resource)
    {
        if (context.User == null)
        {
            return Task.CompletedTask;
        }
        
        if (context.User.IsInRole(Authorization.Constants.SuperAdminsRole) ||
            context.User.IsInRole(Authorization.Constants.AdminsRole))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}