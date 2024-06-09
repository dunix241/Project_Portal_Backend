namespace Domain.EnrollmentPlan;

public class EnrollmentPlanDetails
{
    public Guid Id { get; set; }
    public Guid EnrollmentPlanId { get; set; }
    public EnrollmentPlan EnrollmentPlan { get; set; }
    public Guid ProjectId { get; set; }
    public Project.Project Project { get; set; }
    public Guid PrerequisiteProjectId { get; set; }
    public Project.Project PrerequisiteProject { get; set; }
}