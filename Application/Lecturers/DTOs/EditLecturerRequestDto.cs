using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace Application.Lecturers.DTOs
{
    public class EditLecturerRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Title { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }  
        public Guid SchoolId { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }
    }
}
