using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace Application.Lecturers.DTOs
{
    public class CreateLecturerRequedtDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        [IsEmailUnique]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid SchoolId { get; set; }
    }
}
