namespace Application.EnrollmentPlans.EnrollmentPlanDetails.DTOs;

public class EnrollmentPlanDetailsResponseDto
{
    public Guid Id { get; set; }
    public Guid EnrollmentPlanId { get; set; }
    public Guid ProjectId { get; set; }
    public Guid PrerequisiteProjectId { get; set; }
}