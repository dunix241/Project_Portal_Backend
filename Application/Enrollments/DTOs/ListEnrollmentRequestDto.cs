using Application.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments.DTOs
{
    public class ListEnrollmentRequestDto:PagingParams
    {
        public string? UserId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? SemesterId { get; set; }
        public bool? IsPublished { get; set; }
    }
}
