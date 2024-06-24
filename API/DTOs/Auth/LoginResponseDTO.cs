using Application.Minio.DTOs;

namespace API.DTOs.Accounts;

public class LoginResponseDTO
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public FileResponseDto Avatar { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
}