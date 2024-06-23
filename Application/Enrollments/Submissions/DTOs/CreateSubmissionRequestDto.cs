namespace Application.Enrollments.Submissions.DTOs
{
    public class CreateSubmissionRequestDto
    {
        public Guid EnrollmentId { get; set; }
        public DateTime DueDate { get; set; }
        public string? Description { get; set; }
        public string Status { get; private set; } = SubmissionStatus.UNSUBMITTED;
    }
}
