namespace Application.Enrollments.DTOs;

public class EditEnrollmentRequestDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Vision { get; set; }
    public string? HeirFortunes { get; set; }
    public bool? CanBeForked { get; set; } = true;
}