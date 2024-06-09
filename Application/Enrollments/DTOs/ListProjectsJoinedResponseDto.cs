using Domain.Semester;

namespace Application.Enrollments.DTOs;

public class ListProjectsJoinedResponseDto
{
    private List<ProjectJoinedResponseDto>? _enrollments;

    public List<ProjectJoinedResponseDto> Enrollments
    {
        get => _enrollments ?? new List<ProjectJoinedResponseDto>();
        set => _enrollments = value;
    }
}

public class ProjectJoinedResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public ProjectSemester ProjectSemester { get; set; }
}