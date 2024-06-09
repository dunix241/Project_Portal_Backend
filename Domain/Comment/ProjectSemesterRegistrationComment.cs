using Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Comment
{
    public class ProjectSemesterRegistrationComment : CommentBase
    {
        public Guid ProjectSemesterRegistrationId { get; set; }
        public ProjectEnrollment ProjectSemesterRegistration { get; set; }
    }
}
