using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments.Submissions.Comments.DTOs
{
    public  class CreateSubmissionCommentRequest
    {
        public string Content { get; set; }
        public DateTime Date { get; private set; } = DateTime.UtcNow;
        public Guid SubmissionId { get; set; }
    }
} 
