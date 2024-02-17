namespace Application.Semesters.DTOs.Projects;

public class SemesterCreateProjectRequestDto
{
    public Guid ProjectId { get; set; }
    public int Slots { get; set; }
    public DateTime DueDate { get; set; }
}