using Application.EnrollmentPlans.EnrollmentPlanDetails.ValidationAttributes;

namespace Application.EnrollmentPlans.EnrollmentPlanDetails.DTOs;

public class CreateEnrollmentPlanDetailsRequestDto
{
    [ProjectExists]
    public Guid ProjectId { get; set; }
    [ProjectExists]
    public Guid PrerequisiteProjectId { get; set; }
}