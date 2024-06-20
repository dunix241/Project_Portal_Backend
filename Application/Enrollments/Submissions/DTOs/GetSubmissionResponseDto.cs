using Domain.Comment;
using Domain.Submission;
namespace Application.Enrollments.Submissions.DTOs
{
    public class GetSubmissionResponseDto
    {
        public Guid Id { get; set; }
        public Guid EnrollmentId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public Guid? ThesisId { get; set; }
    }
}
