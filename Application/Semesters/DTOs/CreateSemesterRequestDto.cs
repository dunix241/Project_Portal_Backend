using Domain.Semester;

namespace Application.Semesters.DTOs;

public class CreateSemesterRequestDto
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime StartRegistrationDate { get; set; }
    public DateTime EndRegistrationDate { get; set; }
}