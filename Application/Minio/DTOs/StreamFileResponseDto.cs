namespace Application.Minio.DTOs;
using Domain.File;

public class StreamFileResponseDto
{
    public File File { get; set; }
    public Stream Stream { get; set; }
}