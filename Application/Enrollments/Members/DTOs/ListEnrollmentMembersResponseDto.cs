using Domain.Enrollment;

namespace Application.Enrollments.DTOs;

public class ListEnrollmentMembersResponseDto
{
    public List<EnrollmentMemberResponseDto> EnrollmentMembers { get; set; }
}

public class EnrollmentMemberResponseDto {
    public Guid Id { get; set; }
    public Boolean? IsAccepted { get; set; }
    public string? RejectReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}