namespace Domain.EnrollmentPlan;

public class EnrollmentPlan
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SchoolId { get; set; }
    public School.School School { get; set; }
    public bool IsActive { get; set; }
    public IList<EnrollmentPlanDetails> EnrollmentPlanDetailsList { get; set; }
}