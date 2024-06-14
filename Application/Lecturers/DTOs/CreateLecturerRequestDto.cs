using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace Application.Lecturers.DTOs
{
    public class CreateLecturerRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        [IsEmailUnique]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid SchoolId { get; set; }
    }
}
