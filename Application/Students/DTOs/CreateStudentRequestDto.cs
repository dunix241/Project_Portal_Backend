namespace Application.Students.DTOs
{
    public class CreateStudentRequestDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? SchoolId { get; set; }

    }
}
