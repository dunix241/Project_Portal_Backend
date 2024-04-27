using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace API.DTOs.Accounts;

public class RegisterRequestDTO
{
    [Required]
    [EmailAddress]
    [IsEmailUnique]
    public string Email { get; set; }

    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,20}$", ErrorMessage = "Password should be more complex")]
    public string Password { get; set; }

    [Required] public string Name { get; set; }
    [Required] public string Address { get; set; }
}