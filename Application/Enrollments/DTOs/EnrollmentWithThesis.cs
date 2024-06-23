using Application.Minio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments.DTOs
{
    public class EnrollmentWithThesis:Domain.Enrollment.Enrollment
    {
        public FileResponseDto FileResponseDto { get; set; }
    }
}
