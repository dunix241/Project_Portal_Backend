using Domain.Semester;

namespace Application.Semesters.DTOs;

public class EditSemesterRequestDto
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}