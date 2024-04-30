using Domain.File;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Minio.DTOs
{
    public class AddFileRequestDto
    {
        public IFormFile FormFile { get; set; }
        public string BucketName { get; set; }
        public Guid SourceOwnerId { get; set; }
        public SourceOwnerType SourceOwnerType { get; set; }
        public FileType FileType { get; set; }

    }
    public enum SourceOwnerType
    {
        Lecturer,
        Student,
        Project
    }

}
