namespace API.DTOs.Accounts;

public class ChangePasswordRequestDTO
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}