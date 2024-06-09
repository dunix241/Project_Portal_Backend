using Domain.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Project
{
    public class ProjectEnrollment
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ProjectSemesterId { get; set; }
        public ProjectSemester ProjectSemester { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public string? Feedback { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Stars { get; set; }
        public Guid? ForkedFromProjectId { get; set; }
        public string? HeirFortunes { get; set; }
        public List<string> Tags { get; set; } = new List<string>();

        public IList<Domain.Project.ProjectEnrollmentMember> ProjectEnrollmentMembers { get; set; }

    }
}
