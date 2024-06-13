namespace Application.Minio.DTOs
{
    public class AddFileResponseDto
    {
        public Guid Id { get; set; }
        public Guid Name { get; set; }
        public string DisplayName { get; set; }
        public string? Extension { get; set; }
        public string BucketName { get; set; }
    }
}
