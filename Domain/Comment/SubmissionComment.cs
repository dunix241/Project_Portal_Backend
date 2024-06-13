namespace Domain.Comment
{
    public class SubmissionComment : Comment
    {
        public Guid SubmissionId { get; set; }

        public Submission.Submission Submission { get; set; }
    }
}
