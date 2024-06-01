using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Project
{
    public class ProjectMilestone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SchoolId { get; set; }

        public School.School School { get; set; }
        public IList<ProjectMilestoneDetails> ProjectMilestoneDetails { get; set; }
    }
}

