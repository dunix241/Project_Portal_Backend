namespace Domain.Enrollment;

public class EnrollmentMember
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public Guid EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; }
    public Boolean? IsAccepted { get; set; }
    public string? RejectReason { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}