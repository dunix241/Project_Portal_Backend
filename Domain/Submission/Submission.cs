using Domain.Comment;

namespace Domain.Submission
{
    public class Submission
    {
        public Guid Id { get; set; }
        public Guid EnrollmentId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public DateTime? SubmittedDate { get; set; }

        public Enrollment.Enrollment Enrollment { get; set; }
        public Guid? ThesisId { get; set; }
        public File.File? Thesis { get; set; }
        public ICollection<SubmissionComment> SubmissionComments { get; set; }
    }
}
