using System.ComponentModel.DataAnnotations;
using API.DTOs.Accounts.ValidationAttributes;

namespace Application.Lecturers.DTOs
{
    public class EditLecturerRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [EmailAddress]
        [IsEmailUnique]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }  
    }
}
