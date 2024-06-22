namespace Application.Enrollments.Submissions.DTOs
{
    public class EditSubmissionRequestDto
    {
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
        public string ? Descripstion { get; set; }
        public string? ThesisId { get; set; } 
    }
}
