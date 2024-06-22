using Domain.Lecturer;

namespace Application.Lecturers.DTOs
{
    public class GetLecturerResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string? Title { get; set; }
        public string Headline { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } 
        public Guid? SchoolId { get; set; }
        public string SchoolName { get; set; }
    }
}
