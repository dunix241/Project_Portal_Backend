using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Comment
{
    public class SubmissionComment : CommentBase
    {
        public Guid SubmissionId { get; set; }

        public Submission.Submission Submission { get; set; }
    }
}
