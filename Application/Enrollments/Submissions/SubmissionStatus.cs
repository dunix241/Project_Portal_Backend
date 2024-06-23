using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments.Submissions
{
    public class SubmissionStatus
    {
        public static readonly string UNSUBMITTED = "UnSubmitted";
        public static readonly string SUBMITTED = "Submitted";
        public static readonly string ACCEPTED = "Accepted";
        public static readonly string COMPLETED = "Completed";
        public static readonly string REJECTED = "Rejected";
        public static readonly List<string> StatusList = new List<string> { "UnSubmitted","Submitted", "Accepted" , "Completed", "Rejected" };
    }
}
