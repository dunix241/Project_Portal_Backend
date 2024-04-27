using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace Application.Students.DTOs
{
    public class EditStudentRequestDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        [IsEmailUnique]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
