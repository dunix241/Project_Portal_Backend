using System.Security.Claims;

namespace Application.Interfaces;

public interface IUserAccessor
{
    UserCredentials GetUser();
}

public class UserCredentials
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public ClaimsPrincipal User { get; set; }
}
