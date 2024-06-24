using System.ComponentModel.DataAnnotations.Schema;
using Domain.File;
using Domain.Semester;

namespace Domain.Enrollment;

public class Enrollment
{
    public Guid Id { get; set; }
    public string OwnerId { get; set; }
    public Student.Student Owner { get; set; }
    public Guid ProjectId { get; set; }
    public Guid SemesterId { get; set; }
    public ProjectSemester ProjectSemester { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Vision { get; set; }
    public string? HeirFortunes { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishDate { get; set; }
    public DateTime RegisterDate { get; set; } = DateTime.Now;
    public bool CanBeForked { get; set; }
    public Guid? ForkedFromId { get; set; }
    [ForeignKey("ForkedFromId")]
    public Enrollment? ForkedFrom { get; set; }
    public List<EnrollmentMember> EnrollmentMembers { get; set; }
    public Guid? ThesisId { get; set; }
    public Domain.File.File? Thesis { get; set; }
}
