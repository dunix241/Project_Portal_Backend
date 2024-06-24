namespace Application.Enrollments.Submissions.DTOs
{
    public class CreateSubmissionRequestDto
    {
        public Guid EnrollmentId { get; set; }
        public DateTime DueDate { get; set; }
        public string? Description { get; set; }
    }
}
