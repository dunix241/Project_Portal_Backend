using Domain.Project;

namespace Domain.Semester;

public class ProjectSemester
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Project.Project Project { get; set; }
    public Guid SemesterId { get; set; }
    public Semester Semester { get; set; }
    public int Slots { get; set; }
    public DateTime DueDate { get; set; }
    IList<ProjectEnrollment> ProjectEnrollments { get; set; }
}