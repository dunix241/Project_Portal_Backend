namespace Application.EnrollmentPlans.DTOs;

public class EnrollmentPlanResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SchoolId { get; set; }
    public bool IsActive { get; set; }
}