using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Authorization.Users
{
    public static class UserOperations
    {
        public static OperationAuthorizationRequirement ResetPassword = new OperationAuthorizationRequirement { Name = Constants.ResetPasswordOperationName };
    }

    public class Constants
    {
        public static readonly string ResetPasswordOperationName = "ResetPassword";
    }
}
