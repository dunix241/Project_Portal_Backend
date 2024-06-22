namespace Application.Minio.DTOs;

public class FileResponseDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}