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
        public async Task<IActionResult> UploadFile(IFormFile file, string bucketName, Guid sourceOwnerId, SourceOwnerType sourceOwnerType, FileType fileType)
        {
            var dto = new AddFileRequestDto
            {
                SourceOwnerType = sourceOwnerType,
                BucketName = bucketName,
                FormFile = file,
                SourceOwnerId = sourceOwnerId,
                FileType = fileType
            };

            return HandleResult(await Mediator.Send(new UploadFile_V2.Command { dto = dto }));
        }

        [HttpDelete("Delete")]
        [SwaggerOperation(Summary = "Delete a file in Minio")]
        public async Task<IActionResult> DeleteFile(string bucketName, string objectName)
        {
            var result = await Mediator.Send(new DeleteFile.Command
            {
                BucketName = bucketName,
                ObjectName = objectName
            });
            return HandleResult(result);
        }
        [HttpGet("GetFile")]
        [SwaggerOperation(Summary = "Get File")]
        public async Task<IActionResult> GetStreamFile(string bucketName, string objectName)
        {
            var query = new StreamFile.Query { BucketName = bucketName, ObjectName = objectName };
            var result = await Mediator.Send(query);

            if (result.IsSuccess)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await result.Value.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", objectName);
                }
            }
            else
            {
                return NotFound(result.Error);
            }
        }

        [HttpGet("GetAvatarLink")]
        [SwaggerOperation(Summary = "Get Avatar")]
        public async Task<IActionResult> GetAvatarLink(string bucketName, Guid ownerId, SourceOwnerType sourceOwnerType)
        {
            var query = new GetAvatar.Query { BucketName = bucketName,OwnerId = ownerId,OwnerType= sourceOwnerType };
            return HandleResult(await Mediator.Send(query));       
        }
    }
}
