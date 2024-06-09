namespace Domain.Comment
{
    public class SubmissionComment : CommentBase
    {
        public Guid SubmissionId { get; set; }

        public Submission.Submission Submission { get; set; }
    }
}
