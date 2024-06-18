using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments.DTOs
{
    public class GetEnrollmentHistoryResponseDto
    {
        public Guid SemesterId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; }
    }
    public class EnrollmentMemberDto
    {
        public Guid MemberId { get; set; }
        public string UserId { get; set; }
        public bool? IsAccepted { get; set; }
        public string? RejectReason { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class EnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime RegisterDate { get; set; }
        public List<EnrollmentMemberDto> Members { get; set; }
    }

}
