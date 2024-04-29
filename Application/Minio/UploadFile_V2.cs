using Application.Core;
using Application.Minio.DTOs;
using MediatR;
using Minio.DataModel.Args;
using Minio;
using Persistence;
using File = Domain.File;

namespace Application.Minio
{
    public class UploadFile_V2
    {
        public class Command : IRequest<Result<Unit>>
        {
            public AddFileRequestDto dto;
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IMinioClient _minioClient;
            private readonly DataContext _context;

            public Handler(IMinioClient minioClient, DataContext context)
            {
                _minioClient = minioClient;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var file = request.dto.FormFile;
                    var bucketName = request.dto.BucketName;
                    var sourceOwnerType = request.dto.SourceOwnerType;
                    var sourceOwnerId = request.dto.SourceOwnerId;

                    if (file.Length <= 0)
                    {
                        return Result<Unit>.Failure("No file uploaded or file is empty.");
                    }


                    var existArgs = new BucketExistsArgs().WithBucket(request.dto.BucketName);
                    var found = await _minioClient.BucketExistsAsync(existArgs).ConfigureAwait(false);
                    if (!found)
                    {
                        return Result<Unit>.Failure($"Bucket {request.dto.BucketName} does not exist.");
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        DateTime currentTime = DateTime.UtcNow;
                        var extension = Path.GetExtension(file.FileName);
                        var unixTime = ((DateTimeOffset)currentTime).ToUnixTimeSeconds();
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + $"_{unixTime}{extension}";

                        var fileObj = new File.File
                        {
                            FileOriginalName = file.FileName,
                            FileName = fileName,
                            Extension = extension,
                        };

                        switch (sourceOwnerType)
                        {
                            case SourceOwnerType.Student:
                                var student = await _context.Students.FindAsync(sourceOwnerId);
                                if (student == null)
                                    return Result<Unit>.Failure($"Student ID {sourceOwnerId} does not exist.");
                                fileObj.Student = student;
                                fileObj.StudentId = sourceOwnerId;
                                break;

                            case SourceOwnerType.Lecturer:
                                var lecturer = await _context.Lecturers.FindAsync(sourceOwnerId);
                                if (lecturer == null)
                                    return Result<Unit>.Failure($"Lecturer ID {sourceOwnerId} does not exist.");
                                fileObj.Lecturer = lecturer;
                                fileObj.LecturerId = sourceOwnerId;
                                break;

                            case SourceOwnerType.Project:
                                var project = await _context.Projects.FindAsync(sourceOwnerId);
                                if (project == null)
                                    return Result<Unit>.Failure($"Project ID {sourceOwnerId} does not exist.");
                                fileObj.Project = project;
                                fileObj.ProjectId = sourceOwnerId;
                                break;

                            default:
                                return Result<Unit>.Failure($"Error searching for owner target");
                        }


                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        var putArgs = new PutObjectArgs()
                            .WithBucket(request.dto.BucketName)
                            .WithObject(fileName)
                            .WithStreamData(memoryStream)
                            .WithObjectSize(memoryStream.Length)
                            .WithContentType("application/octet-stream")
                          ; 
                        await _minioClient.PutObjectAsync(putArgs);
                        _context.Files.Add(fileObj);
                        var success = await _context.SaveChangesAsync() != 0;
                        if (!success)
                        {
                            return Result<Unit>.Failure($"Error adding File Object to DB");
                        }

                        return Result<Unit>.Success(Unit.Value);

                    }                  
                }
                catch (Exception ex)
                {
                    return Result<Unit>.Failure($"Error uploading file: {ex.Message}");
                }
            }
        }
    }
}
