using Domain.Semester;

namespace Domain.Project;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IList<ProjectSemester> ProjectSemesters { get; set; }
    public IList<Image> Images { get; set; }
}