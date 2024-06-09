namespace Domain.Semester;

public class Semester
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime StartRegistrationDate { get; set; }
    public DateTime EndRegistrationDate { get; set; }
    public ICollection<ProjectSemester> ProjectSemesters { get; set; }
}