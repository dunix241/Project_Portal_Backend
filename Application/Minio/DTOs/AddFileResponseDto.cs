using Domain.File;
using Domain.Lecturer;
using Domain.Project;
using Domain.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Minio.DTOs
{
    public class AddFileResponseDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileOriginalName { get; set; }
        public FileType FileType { get; set; }

        public Guid? StudentId { get; set; }
        public Guid? LecturerId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
