

namespace Domain.Project
{
    public class ProjectMilestoneDetails
    {
        public Guid Id { get; set; }
        public Guid ProjectMilestoneId { get; set; }
        public Guid ProjectId { get; set; }
        public string Prerequisite { get; set; }

        public ProjectMilestone ProjectMilestone { get; set; }
        public Project Project { get; set; }
    }
}
