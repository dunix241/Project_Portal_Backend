using Microsoft.AspNetCore.Identity;

namespace Domain;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string? Bio { get; set; }
    public Image? Avatar { get; set; }
}