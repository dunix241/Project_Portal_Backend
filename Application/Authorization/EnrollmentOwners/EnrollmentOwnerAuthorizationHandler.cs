using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Application.Authorization.EnrollmentOwners;

public class EnrollmentOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Domain.Enrollment.Enrollment>
{
    private readonly UserManager<User> _userManager;

    public EnrollmentOwnerAuthorizationHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
        Domain.Enrollment.Enrollment resource)
    {
        if (context.User == null || resource == null)
        {
            return Task.CompletedTask;
        }

        if (resource.OwnerId == _userManager.GetUserId(context.User))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}