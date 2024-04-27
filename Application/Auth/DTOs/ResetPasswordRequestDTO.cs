using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Accounts;

public class ResetPasswordRequestDTO
{
    [EmailAddress]
    public string Email { get; set; }
}