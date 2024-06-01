using Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectMilestoneDetail.DTOs
{
    public class CreateProjectMilestoneDetailRequest
    {
        public Guid Id { get; set; }
        public Guid ProjectMilestoneId { get; set; }
        public Guid ProjectId { get; set; }
        public string Prerequisite { get; set; }
    }
}
