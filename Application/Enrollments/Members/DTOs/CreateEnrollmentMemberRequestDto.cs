using System.ComponentModel.DataAnnotations;

namespace Application.Enrollments.DTOs;

public class CreateEnrollmentMemberRequestDto
{
    [EmailAddress]
    public string Email { get; set; }
}