using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectMilestone.DTOs
{
    public class EditProjectMilestoneRequestDto
    {
        public string Name { get; set; }
        public Guid SchoolId { get; set; }
    }
}
