using Microsoft.AspNetCore.Http;


namespace Application.Minio.DTOs
{
    public class AddFileRequestDto
    {
        public IFormFile FormFile { get; set; }
        public string BucketName { get; set; }
    }


}
