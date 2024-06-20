namespace Application.Enrollments.Submissions.DTOs
{
    public class CreateSubmissionRequestDto
    {
        public Guid EnrollmentId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; private set; } = "unSubmitted";
        public DateTime? SubmittedDate { get; private set; } = DateTime.UtcNow;
    }
}
