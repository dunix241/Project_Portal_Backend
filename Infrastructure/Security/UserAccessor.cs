using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Security;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserCredentials GetUser()
    {
        var userCredentials = new UserCredentials
        {
            Username = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name),
            Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
            Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email),
            User = _httpContextAccessor.HttpContext.User
        };
        
        return userCredentials;
    }
}
