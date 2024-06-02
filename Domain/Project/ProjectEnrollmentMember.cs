using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Project
{
    public class ProjectEnrollmentMember
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool? IsApproved { get; set; }
        public string RejectReason { get; set; }
        public Guid ProjectEnrollmentId { get; set; }
        public ProjectEnrollment ProjectEnrollment { get; set; }

    }
}
