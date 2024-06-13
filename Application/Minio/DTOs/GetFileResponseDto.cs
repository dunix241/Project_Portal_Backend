using File = Domain.File.File;

namespace Application.Minio.DTOs
{
    public class GetFileResponseDto
    {
        public File File { get; set; }
    }
}
