using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace API.DTOs.Accounts;

public class RegisterRequestDTO
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]{4,8}$", ErrorMessage = "Username should contain only letters and numbers between 4 and 8 characters in length")]
    [IsUsernameUnique]
    public string UserName { get; set; }

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