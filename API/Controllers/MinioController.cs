using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;
using Minio;
using Application.Minio;
using System.IO;
using System.Reflection;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Semester;
using Application.Minio.DTOs;
using Domain.File;
using Swashbuckle.AspNetCore.Annotations;
using Domain.Enum;

namespace API.Controllers
{
    public class MinioController : ApiController
    {
        private readonly IMinioClient minioClient;

        public MinioController(IMinioClient minioClient)
        {
            this.minioClient = minioClient;
        }
        [HttpPost("Upload")]
        [SwaggerOperation(Summary = "Upload file to Minio")]
        public async Task<IActionResult> UploadFile(IFormFile file, string bucketName, Guid sourceOwnerId, SourceOwnerType sourceOwnerType)
        {
            var payload = new AddFileRequestDto
            {
                BucketName = bucketName,
                FormFile = file,
            };

            return HandleResult(await Mediator.Send(new UploadFile.Command { Payload = payload }));
        }

        [HttpDelete("Delete")]
        [SwaggerOperation(Summary = "Delete a file in Minio")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            var result = await Mediator.Send(new DeleteFile.Command
            {
                Id = id
            });
            return HandleResult(result);
        }
        [HttpGet("GetFile")]
        [SwaggerOperation(Summary = "Get File")]
        public async Task<IActionResult> GetStreamFile(Guid id)
        {
            var query = new StreamFile.Query {Id = id};
            var result = await Mediator.Send(query);

            if (result.IsSuccess)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await result.Value.Stream.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.Value.File.FileNameWithExtension);
                }
            }
            else
            {
                return NotFound(result.Error);
            }
        }
    }
}
