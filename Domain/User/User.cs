using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public Guid? AvatarId { get; set; }
    public File.File Avatar { get; set; }
    public bool IsActive { get; set; } = true;
}