using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enrollment
{
    public class EnrollmentEnvaluation
    {
        public Guid Id { get; set; }
        public Guid EnrollmentId { get; set; }
        public string SupervisorId { get; set; }
        public string Presentation { get; set; }
        public string Content { get; set; }
        public string Outcome { get; set; }
        public string QAPerformance { get; set; }
        public string? SuggestionForImprovement { get; set; }
    }
}
