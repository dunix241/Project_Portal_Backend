namespace API.DTOs.Accounts;

public class LoginResponseDTO
{
    public string Name { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Avatar { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
}