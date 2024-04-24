using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Minio.DTOs
{
    public class AddFileRequestDto
    {
        public string FilePath { get; set; }
        public string BucketName { get; set; }
        public string ObjectName { get; set; }
        public string NewName { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }

        public Guid EntityId { get; set; }
        public string EntityType { get; set; }
    }
}
