namespace Application.Lecturers.DTOs
{
    public class CreateLecturerRequedtDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid SchoolId { get; set; }
    }
}
