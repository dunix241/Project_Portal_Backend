using Application.Enrollments.ValidationAttributes;

namespace Application.Enrollments.DTOs;

public class CreateEnrollmentRequestDto
{
    public Guid ProjectId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Vision { get; set; }
    [EmailAddresses]
    public List<string> Emails { get; set; }
}
