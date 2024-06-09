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
    // public string Name { get; set; }
    public string? Address { get; set; }
    public string? Bio { get; set; }
    public Image? Avatar { get; set; }
}