namespace Application.Enrollments.DTOs
{
    public class GetEnrollmentHistoryResponseDto
    {
        public Guid SemesterId { get; set; }
        public string Name { get; set; }
        public Guid EnrollmentId { get; set; }
    }
}
