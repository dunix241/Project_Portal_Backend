namespace Application.EnrollmentPlans.DTOs;

public class EditEnrollmentPlanRequestDto
{
    public string Name { get; set; }
    public Guid SchoolId { get; set; }
    public bool IsActive { get; set; }
}