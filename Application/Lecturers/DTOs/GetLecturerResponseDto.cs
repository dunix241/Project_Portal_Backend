using Domain.Lecturer;

namespace Application.Lecturers.DTOs
{
    public class GetLecturerResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }  
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
