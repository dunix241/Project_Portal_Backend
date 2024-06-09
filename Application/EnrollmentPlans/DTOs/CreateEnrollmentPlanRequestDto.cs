using Application.EnrollmentPlans.ValidationAttributes;

namespace Application.EnrollmentPlans.DTOs;

public class CreateEnrollmentPlanRequestDto
{
    public string Name { get; set; }
    [SchoolExists]
    public Guid SchoolId { get; set; }
    public bool IsActive { get; set; }
}