namespace Domain.Thesis
{
    public class Thesis : File.File
    {
        public Guid SubmissionId { get; set; }
        public Submission.Submission Submission { get; set; }
    }
}
