namespace Domain.Semester;

public class Semester
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime RegisterFrom { get; set; }
    public DateTime RegisterTo { get; set; }
    public DateTime DueDate { get; set; }

    public ICollection<ProjectSemester> ProjectSemesters { get; set; }
}