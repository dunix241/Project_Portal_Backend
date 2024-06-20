using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments.Submissions
{
    public class SubmissionStatus
    {
        public static readonly string DEFAULT = "unSubmitted";
        public static readonly string SUBMITTED = "submitted";
        public static readonly string ACCEPTED = "accepted";
        public static readonly string COMPLETED = "completed";
        public static readonly string REJECTED = "rejected";
        public static readonly List<string> StatusList = new List<string> { "submitted", "accepted" , "completed", "rejected" };
    }
}
