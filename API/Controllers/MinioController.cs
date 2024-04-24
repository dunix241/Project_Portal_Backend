using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Args;
using Minio;
using Application.Minio;
using System.IO;
using System.Reflection;
using Application.Minio.DTOs;

namespace API.Controllers
{
    public class MinioController : ApiController
    {
        private readonly IMinioClient minioClient;

        public MinioController(IMinioClient minioClient)
        {
            this.minioClient = minioClient;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file, string bucketName)
        {
            string filename = file.Name;
            string fileExtension = "." + file.ContentType.Split('/')[1];

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            try
            {
                string tempFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
                if (Directory.Exists(tempFolderPath))
                {
                    Directory.Delete(tempFolderPath, true);
                    Directory.CreateDirectory(tempFolderPath);
                }

                var randomName = Path.GetRandomFileName();
                string tempFilePath = Path.Combine(tempFolderPath, randomName);

                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var request = new AddFileRequestDto
                {
                    FilePath = tempFilePath,
                    BucketName = bucketName,
                    ObjectName = file.FileName,
                    NewName = randomName,
                    Extension = fileExtension,
                    FileName = file.FileName,
                };

                var result = await Mediator.Send(new UploadFile.Command { dto = request });

                if (!result.IsSuccess)
                {
                    return BadRequest(result.Error);
                }

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error uploading file: {ex.Message}");
            }
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFile(string bucketName, string objectName)
        {
            try
            {
                var result = await Mediator.Send(new DeleteFile.Command
                {
                    BucketName = bucketName,
                    ObjectName = objectName
                });

                if (result.IsSuccess)
                {
                    return Ok("File deleted successfully.");
                }
                else
                {
                    return BadRequest($"Error deleting file: {result.Error}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting file: {ex.Message}");
            }
        }


        [HttpGet("GetFile")]
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


    }
}
