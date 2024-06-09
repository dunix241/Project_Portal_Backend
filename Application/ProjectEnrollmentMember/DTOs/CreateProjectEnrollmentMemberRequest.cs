using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectEnrollmentMember.DTOs
{
    public  class CreateProjectEnrollmentMemberRequest
    {
        public string Email { get; set; }
        public Guid ProjectEnrollmentId { get; set; }
    }
}
