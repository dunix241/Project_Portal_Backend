using Domain.File;
using File = Domain.File;


namespace Application.Minio.DTOs
{
    public class GetFileResponseDto
    {
        public File.File File { get; set; }
    }
}
