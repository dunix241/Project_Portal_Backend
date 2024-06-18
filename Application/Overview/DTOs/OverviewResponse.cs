using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Overview.DTOs
{
    public class OverviewResponse
    {
        public List<Domain.Enrollment.Enrollment> CurrentSemesterEnrollments { get; set; }
        public List<Domain.Project.Project> CurrentAvaiableProjects { get; set; }
        public List<SubmissionStatusCount> SubmissionStatusCount { get; set; }
        public List<Domain.Enrollment.Enrollment> RecentSemesterEnrollments { get; set; }
        public int EnrollmentCount { get; set; }
        public int PublishEnrollmentCount { get; set; }
        public double SemesterProgress { get; set; }
        public int StudentEnrollThisSemester { get; set; }
        public int StudentEnrollPreviousSemester { get; set; }


    }
    public class SubmissionStatusCount
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }
}
