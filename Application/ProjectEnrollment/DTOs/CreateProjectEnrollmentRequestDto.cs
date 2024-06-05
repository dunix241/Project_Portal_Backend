using Domain.Semester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectEnrollment.DTOs
{
    public class CreateProjectEnrollmentRequestDto
    {
        public Guid UserId { get; set; }
        public Guid ProjectSemesterId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Vision { get; set; }
        public string Mission { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Stars { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
