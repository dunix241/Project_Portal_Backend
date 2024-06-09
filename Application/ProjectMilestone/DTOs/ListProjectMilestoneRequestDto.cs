using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectMilestone.DTOs
{
    public  class ListProjectMilestoneRequestDto
    {
        public Guid? SchoolId { get; set; }
        public string? Name { get; set; }

    }
}
