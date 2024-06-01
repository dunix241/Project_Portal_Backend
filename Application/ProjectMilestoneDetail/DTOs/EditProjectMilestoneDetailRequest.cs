using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectMilestoneDetail.DTOs
{
    public class EditProjectMilestoneDetailRequest
    {
        public Guid ProjectMilestoneId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
