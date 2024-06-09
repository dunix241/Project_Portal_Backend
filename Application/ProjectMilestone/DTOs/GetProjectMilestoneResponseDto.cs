using Project = Domain.Project;

namespace Application.ProjectMilestone.DTOs
{
    public class GetProjectMilestoneResponseDto
    {
       public Project.ProjectMilestone ProjectMilestone { get; set; }
    }
}
