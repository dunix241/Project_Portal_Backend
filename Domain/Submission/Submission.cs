using Domain.Comment;
using Domain.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Submission
{
    public class Submission
    {
        public Guid Id { get; set; }
        public Guid EnrollmentId { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public DateTime? SubmittedDate { get; set; }

        public ProjectEnrollment Enrollment { get; set; }
        public Thesis.Thesis Thesis { get; set; }
        public ICollection<SubmissionComment> SubmissionComments { get; set; }
    }
}
