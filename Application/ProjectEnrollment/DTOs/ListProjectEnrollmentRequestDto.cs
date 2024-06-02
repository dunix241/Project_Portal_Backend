using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectEnrollment.DTOs
{
    public class ListProjectEnrollmentRequestDto
    {
        public Guid? SemesterId { get; set; }
        public string? Name { get; set; }
        public Guid? SchoolId { get; set; }

    }
}
