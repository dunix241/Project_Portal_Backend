using Domain.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Minio.DTOs
{
    public class EditFileRequestDto
    {
        public string FileOriginalName { get; set; }
        public FileType FileType { get; set; }
    }
}
