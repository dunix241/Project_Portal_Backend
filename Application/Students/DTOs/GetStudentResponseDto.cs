namespace Application.Students.DTOs
{
    public class GetStudentResponseDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long IRN { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } 
        public Guid SchoolId { get; set; }
        public string? SchoolName { get; set; }
    }
}
