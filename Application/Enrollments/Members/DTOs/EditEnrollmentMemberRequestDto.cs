namespace Application.Enrollments.DTOs;

public class EditEnrollmentMemberRequestDto
{
    public Boolean? IsAccepted { get; set; }
    public string? RejectReason { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}