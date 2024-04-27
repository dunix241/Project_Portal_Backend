using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace Application.Students.DTOs
{
    public class CreateStudentRequestDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [IsEmailUnique]
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? SchoolId { get; set; }

    }
}
